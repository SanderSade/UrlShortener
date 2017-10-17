using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseConverter.Test
{
	[TestClass]
	public class BaseConversionTests
	{
		[TestMethod]
		public void ConvertString()
		{
			var baseConverter = new BaseConversion.BaseConverter(16);
			Assert.AreEqual(16, baseConverter.Base);
			var value = baseConverter.Convert("7E");
			Trace.WriteLine(value);
			Assert.AreEqual(0x7E, value.DecimalValue);
			Assert.AreEqual("7E", value.BaseValue);
		}

		[ExpectedException(typeof(ApplicationException))]
		[TestMethod]
		public void ConvertString_Bad()
		{
			var baseConverter = new BaseConversion.BaseConverter(16);
			baseConverter.Convert("K2HD");
		}


		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void ConvertString_Empty()
		{
			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF");
			baseConverter.Convert("");
		}


		[TestMethod]
		public void ConvertString_Negative()
		{
			var baseConverter = new BaseConversion.BaseConverter(16);
			Assert.AreEqual(16, baseConverter.Base);
			var value = baseConverter.Convert("-7E");
			Trace.WriteLine(value);
			Assert.AreEqual(-0x7E, value.DecimalValue);
			Assert.AreEqual("-7E", value.BaseValue);
		}

		[TestMethod]
		public void ConvertString_Max()
		{
			var baseConverter = new BaseConversion.BaseConverter(16);
			Assert.AreEqual(16, baseConverter.Base);
			var value = baseConverter.Convert(long.MaxValue);
			Trace.WriteLine(value);
		}

		[TestMethod]
		public void ConvertString_Confuse()
		{
			var baseConverter = new BaseConversion.BaseConverter("9876543210");
			var value = baseConverter.Convert("123");
			Trace.WriteLine(value);
			Assert.AreEqual(876, value.DecimalValue);
		}


		[TestMethod]
		public void ConvertString_Base10()
		{
			var baseConverter = new BaseConversion.BaseConverter(10);
			var value = baseConverter.Convert("123");
			Trace.WriteLine(value);
			Assert.AreEqual(123, value.DecimalValue);
		}

		[TestMethod]
		public void ConvertString_0()
		{
			var baseConverter = new BaseConversion.BaseConverter("abBA");
			var value = baseConverter.Convert("a");
			Trace.WriteLine(value);
			Assert.AreEqual(0, value.DecimalValue);
		}

		[ExpectedException(typeof(OverflowException))]
		[TestMethod]
		public void ConvertString_Overflow()
		{
			var baseConverter = new BaseConversion.BaseConverter(Enumerable.Range(0, 254).Select(x => (char)x));
			Assert.AreEqual(254, baseConverter.Base);
			var value = baseConverter.Convert(" ");
			Assert.AreEqual(32, value.DecimalValue);
			Trace.WriteLine(value);
			baseConverter.Convert("Lucy in the sky with diamonds");
		}


		[TestMethod]
		public void ConvertString_Ascii()
		{
			var baseConverter = new BaseConversion.BaseConverter(Enumerable.Range(0, 256).Select(x => (char)x));
			Assert.AreEqual(256, baseConverter.Base);
			var value = baseConverter.Convert("Nothing");
			Trace.WriteLine(value);
			Assert.AreEqual(22077593942060647, value.DecimalValue);


			value = baseConverter.Convert("Hello Dolly");
			Trace.WriteLine(value);
			Assert.AreEqual(7813499356810341497, value.DecimalValue);


			value = baseConverter.Convert(long.MaxValue);
			Trace.WriteLine(value);
		}

		[TestMethod]
		public void ConvertString_AlphaNumeric()
		{
			var list = new List<char>();
			list.AddRange(" abcdefghijklmnopqrstuvwxyz".ToUpperInvariant().ToCharArray());
			list.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());
			list.AddRange("0123456789".ToCharArray());


			var baseConverter = new BaseConversion.BaseConverter(list);
			var value = baseConverter.Convert("Hello Dolly");
			Trace.WriteLine(value);
			Assert.AreEqual(8373672738368257056, value.DecimalValue);
		}


		[TestMethod]
		public void ConvertDecimal()
		{
			var baseConverter = new BaseConversion.BaseConverter(16);
			Assert.AreEqual(16, baseConverter.Base);
			var value = baseConverter.Convert(0x7E);
			Trace.WriteLine(value);
			Assert.AreEqual(0x7E, value.DecimalValue);
			Assert.AreEqual("7E", value.BaseValue);
		}

		[TestMethod]
		public void ConvertDecimal_Binary()
		{
			var baseConverter = new BaseConversion.BaseConverter("01");
			Assert.AreEqual(2, baseConverter.Base);
			var value = baseConverter.Convert(45);
			Trace.WriteLine(value);
			Assert.AreEqual(45, value.DecimalValue);
			Assert.AreEqual("101101", value.BaseValue);
		}


		[TestMethod]
		public void ConvertDecimal_0()
		{
			var baseConverter = new BaseConversion.BaseConverter("XY");
			Assert.AreEqual(2, baseConverter.Base);
			var value = baseConverter.Convert(0);
			Trace.WriteLine(value);
			Assert.AreEqual(0, value.DecimalValue);
			Assert.AreEqual("X", value.BaseValue);
		}



		[TestMethod]
		public void ConvertDecimal_Negative()
		{
			var baseConverter = new BaseConversion.BaseConverter("XY");
			Assert.AreEqual(2, baseConverter.Base);
			var value = baseConverter.Convert(-45);
			Trace.WriteLine(value);
			Assert.AreEqual(-45, value.DecimalValue);
			Assert.AreEqual("-YXYYXY", value.BaseValue);
		}


		[TestMethod]
		public void Convert_BackAndForth()
		{
			var chars = "AbBC";
			var baseConverter = new BaseConversion.BaseConverter(chars);
			Assert.AreEqual(chars.Length, baseConverter.Base);


			long[] values = { 0, 992, 129, -126, 90909090, -90, -0, 11234, -11, 22, int.MaxValue * 10000000L, long.MaxValue};

			foreach (long i in values)
			{
				var value = baseConverter.Convert(i);
				var value2 = baseConverter.Convert(value.BaseValue);
				Trace.WriteLine($"{i}: {value} | {value2}");
				Assert.AreEqual(i, value2.DecimalValue);
			}
		}
		[TestMethod]
		public void ConvertString_MdSamples()
		{
			var list = new List<char>();
			list.AddRange("0123456789".ToCharArray());
			list.AddRange("abcdefghijklmnopqrstuvwxyz".ToUpperInvariant().ToCharArray());
			list.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());

			var baseConverter = new BaseConversion.BaseConverter(list);
			Trace.WriteLine(baseConverter.Base);
			Trace.WriteLine(baseConverter.Convert(long.MaxValue));
			Trace.WriteLine(baseConverter.Convert(934556467467656));

			list.Clear();

			list.AddRange("0123456789".ToCharArray());
			list.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());

			baseConverter = new BaseConversion.BaseConverter(list);
			Trace.WriteLine(baseConverter.Base);
			Trace.WriteLine(baseConverter.Convert(long.MaxValue));
			Trace.WriteLine(baseConverter.Convert(long.MinValue));
			Trace.WriteLine(baseConverter.Convert("76pb94"));
		}

	}
}

