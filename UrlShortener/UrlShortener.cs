using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShortUrl
{
	/// <summary>
	/// Entry point
	/// </summary>
	public sealed class UrlShortener
	{
		/// <summary>
		/// Character values
		/// </summary>
		private readonly Dictionary<char, int> _charValues;

		/// <summary>
		/// Current value
		/// </summary>
		private long _position;


		/// <summary>
		/// Specify characters to use and starting point
		/// </summary>
		/// <param name="characters">Characters. All valid C# characters are acceptable. Cannot contain duplicates</param>
		/// <param name="initialValue">Initial start point for "Next" and "Current"</param>
		public UrlShortener(string characters, long initialValue = 0) : this(characters.ToCharArray(), initialValue)
		{
		}

		/// <summary>
		/// Get specific numeral system. This is supported up to base 36 (0..9, A..Z), beyond that you have to define the symbols yourself
		/// </summary>
		/// <param name="radix"></param>
		/// <param name="initialValue"></param>
		public UrlShortener(int radix, long initialValue = 0) : this(GetBaseCharacters(radix), initialValue)
		{
		}


		private static IEnumerable<char> GetBaseCharacters(int radix)
		{
			const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			if (radix > characters.Length)
				throw new ArgumentOutOfRangeException($"Maximum supported radix (base) is {characters.Length} using this constructor. Use another constructor, defining the symbols yourself");

			return characters.Take(radix);
		}


		/// <summary>
		/// Specify characters to use and starting point
		/// </summary>
		/// <param name="characters">Characters. All valid C# characters are acceptable. Cannot contain duplicates</param>
		/// <param name="initialValue">Initial start point for "Next" and "Current"</param>
		public UrlShortener(IEnumerable<char> characters, long initialValue = 0)
		{
			if (characters == null)
				throw new ArgumentNullException(nameof(characters));

			var chars = characters.ToArray();

			if (chars.Length < 2)
				throw new ArgumentNullException(nameof(characters));

			if (chars.Length != chars.Distinct().Count())
				throw new ApplicationException("Specified characters contain duplicates!");

			_position = initialValue;
			Characters = chars;

			_charValues = new Dictionary<char, int>(chars.Length);
			for (var i = 0; i < chars.Length; i++)
			{
				_charValues[chars[i]] = i;
			}
		}


		/// <summary>
		/// Characters specified in the constructor
		/// </summary>
		public char[] Characters { get; }

		/// <summary>
		/// Current base for conversions and functionality
		/// </summary>
		public int Base => Characters.Length;

		/// <summary>
		/// Current value in sequence
		/// <para>This does not move to the next value in sequence</para>
		/// <para>Thread-safe</para>
		/// </summary>
		public Value Current
		{
			get
			{
				lock (this)
				{
					return Convert(_position);
				}
			}
		}

		/// <summary>
		/// Fetches the next value in sequence
		/// <para>This moves the current to next value, e.g. Current becomes Current + 1</para>
		/// <para>Thread-safe</para>
		/// </summary>
		public Value Next
		{
			get
			{
				lock (this)
				{
					return Convert(++_position);
				}
			}
		}



		/// <summary>
		/// Fetches the previous value in sequence
		/// <para>This moves the current to previous value, e.g. Current becomes Current - 1</para>
		/// <para>Thread-safe</para>
		/// </summary>
		public Value Previous
		{
			get
			{
				lock (this)
				{
					return Convert(--_position);
				}
			}
		}


		/// <summary>
		/// Convert decimal system value to specified base
		/// </summary>
		public Value Convert(long decimalValue)
		{
			if (decimalValue == 0)
				return new Value(0, _charValues.First(x => x.Value == 0).Key.ToString());

			var sb = new StringBuilder();
			var n = decimalValue;
			while (n != 0)
			{
				n = Math.DivRem(n, Base, out var remainder);
				sb.Append(_charValues.First(x => x.Value == Math.Abs(remainder)).Key);
			}
			if (decimalValue < 0)
				sb.Append("-");
			var baseValue = new string(sb.ToString().Reverse().ToArray());
			return new Value(decimalValue, baseValue);
		}


		/// <summary>
		/// Convert specified base value to decimal system
		/// </summary>
		public Value Convert(string baseValue)
		{
			if (string.IsNullOrEmpty(baseValue))
				throw new ArgumentNullException(nameof(baseValue), $"{nameof(baseValue)} must be set");

			var original = baseValue;

			var isNegative = false;

			if (baseValue.StartsWith("-"))
			{
				isNegative = true;
				baseValue = baseValue.Substring(1);
			}

			var charArray = baseValue.ToCharArray();
			var invalidCharacters = charArray.Except(Characters).ToArray();
			if (invalidCharacters != null && invalidCharacters.Length > 0)
				throw new ApplicationException($"Invalid characters in input: {string.Join(string.Empty, invalidCharacters)}");

			var total = 0L;

			checked
			{
				for (var i = 0; i < charArray.Length; i++)
				{
					//math.pow gives double, casting that straight to long truncates it, giving invalid values
					//total += (long)Math.Round(_charValues[charArray[i]] * Math.Pow(Base, charArray.Length - i - 1L), 0);
					total += _charValues[charArray[i]] * Pow(Base, charArray.Length - i - 1);
				}
			}
			return new Value(isNegative ? -total : total, original);
		}

		/// <summary>
		/// Math.Pow() works on doubles, which inevitable means rounding and casting
		/// <para>This is probably slower even though it works on nonfractional numbers, but no casting, Math.Round() or rounding isses</para>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private long Pow(long number, int power)
		{
			long result = 1;
			for (long i = 0; i < power; i++)
				result *= number;
			return result;

		}

		/// <summary>
		/// Convert value between decimal system and specified base.
		/// <para>Specify one in the input and returned <see cref="Value"/> will have both</para>
		/// <para>If both are set, the Value is returned without modification!</para>
		/// <para>This does not affect Current/Next/Previous functionality</para>
		/// <para>Invalid base values or larger than <see cref="long.MaxValue"/> will cause an exception!</para>
		/// </summary>
		public Value Convert(Value value)
		{
			if (value.DecimalValue.HasValue && string.IsNullOrEmpty(value.BaseValue))
				return Convert(value.DecimalValue.Value);

			if (!string.IsNullOrEmpty(value.BaseValue) && !value.DecimalValue.HasValue)
				return Convert(value.BaseValue);

			return value;
		}

		/// <summary>
		/// Convert specified base value to decimal system
		/// </summary>
		public long ToInt64(string baseValue)
		{
			return Convert(baseValue).DecimalValue ?? 0;
		}

		/// <summary>
		/// Convert decimal system value to current base
		/// </summary>
		public string FromInt64(long value)
		{
			return Convert(value).BaseValue;
		}

	}
}
