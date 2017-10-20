using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ShortUrl
{
	/// <summary>
	/// Entry point
	/// </summary>
	public sealed class UrlShortener
	{
		private readonly string _characterString;
		private readonly Regex _validator;

		/// <summary>
		/// Current value
		/// </summary>
		private long _position;


		/// <summary>
		/// Specify characters to use and starting point
		/// </summary>
		/// <param name="characterString">Characters. All valid C# characters are acceptable. Cannot contain duplicates</param>
		/// <param name="initialValue">Initial start point for "Next" and "Current"</param>
		public UrlShortener(string characterString, long initialValue = 0)
		{
			if (string.IsNullOrWhiteSpace(characterString) || characterString.Length < 2)
				throw new ArgumentNullException(nameof(characterString));

			if (characterString.Length != characterString.Distinct().Count())
				throw new ApplicationException("Specified characters contain duplicates!");

			_position = initialValue;
			Characters = characterString.ToCharArray();
			_characterString = characterString;
			_validator = new Regex($@"^[{characterString}]+$", RegexOptions.Compiled);
		}


		/// <summary>
		/// Get specific numeral system. This is supported up to base 62 (0..9, A..Z, a..z), beyond that you have to define the symbols yourself
		/// </summary>
		/// <param name="radix"></param>
		/// <param name="initialValue"></param>
		public UrlShortener(int radix, long initialValue = 0) : this(GetBaseCharacters(radix), initialValue)
		{
		}


		/// <summary>
		/// Specify characters to use and starting point
		/// </summary>
		/// <param name="characters">Characters. All valid C# characters are acceptable. Cannot contain duplicates</param>
		/// <param name="initialValue">Initial start point for "Next" and "Current"</param>
		public UrlShortener(IEnumerable<char> characters, long initialValue = 0) : this(new string(characters.ToArray()), initialValue)
		{
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


		private static string GetBaseCharacters(int radix)
		{
			if (radix > CharacterSet.Base62NumbersUpperLower.Length)
				throw new ArgumentOutOfRangeException(
					$"Maximum supported radix (base) for this constructor is {CharacterSet.Base62NumbersUpperLower.Length}. Use another constructor, defining the symbols yourself");

			return CharacterSet.Base62NumbersUpperLower.Substring(0, radix);
		}


		/// <summary>
		/// Convert decimal system value to specified base
		/// </summary>
		public Value Convert(long decimalValue)
		{
			if (decimalValue == 0)
				return new Value(0, _characterString[0].ToString());

			var l = new List<char>(16);
			var n = decimalValue;
			while (n != 0)
			{
				n = Math.DivRem(n, Base, out var remainder);
				l.Add(_characterString[(int) Math.Abs(remainder)]);
			}

			if (decimalValue < 0)
				l.Add('-');

			l.Reverse();
			return new Value(decimalValue, new string(l.ToArray()));
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

			if (baseValue[0] == '-')
			{
				isNegative = true;
				baseValue = baseValue.Substring(1);
			}

			if (!_validator.IsMatch(baseValue))
				throw new ApplicationException($"Invalid characters in input: {baseValue}");

			var charArray = baseValue.ToCharArray();
			var total = 0L;

			checked
			{
				for (var i = 0; i < charArray.Length; i++)
				{
					//math.pow gives double, casting that straight to long truncates it, giving invalid values
					total += _characterString.IndexOf(charArray[i]) * Pow(Base, charArray.Length - i - 1);
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
