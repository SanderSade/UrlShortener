using System.Numerics;

namespace ShortUrl
{
	/// <summary>
	/// Value in decimal and specified base
	/// </summary>
	public struct Value
	{
		/// <summary>
		/// Create <see cref="Value"/> specifying decimal value
		/// </summary>
		/// <param name="decimalValue"></param>
		public Value(BigInteger? decimalValue) : this()
		{
			DecimalValue = decimalValue;
		}


		/// <summary>
		/// Create <see cref="Value"/> specifying base value
		/// </summary>
		/// <param name="baseValue"></param>
		public Value(string baseValue) : this()
		{
			BaseValue = baseValue;
		}

		/// <summary>
		/// For internal use.
		/// </summary>
		/// <param name="decimalValue"></param>
		/// <param name="baseValue"></param>
		internal Value(BigInteger decimalValue, string baseValue)
		{
			DecimalValue = decimalValue;
			BaseValue = baseValue;
		}


		/// <summary>
		/// Value in base-10 system
		/// </summary>
		public BigInteger? DecimalValue { get; }

		/// <summary>
		/// Value in specified base
		/// </summary>
		public string BaseValue { get; }


		/// <inheritdoc />
		public override string ToString()
		{
			return $"{DecimalValue}: {BaseValue}";
		}
	}
}
