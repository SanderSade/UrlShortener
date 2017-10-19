using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortUrl.Test
{
	[TestClass]
	public class BaseConversionTests
	{
		[TestMethod]
		public void ConvertString()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(16, urlShortener.Base);
			var value = urlShortener.Convert("7E");
			Trace.WriteLine(value);
			Assert.AreEqual(0x7E, value.DecimalValue);
			Assert.AreEqual("7E", value.BaseValue);
		}

		[ExpectedException(typeof(ApplicationException))]
		[TestMethod]
		public void ConvertString_Bad()
		{
			var urlShortener = new UrlShortener(10);
			Trace.WriteLine(urlShortener.Convert("k11"));
		}


		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void ConvertString_Empty()
		{
			var urlShortener = new UrlShortener("0123456789ABCDEF");
			urlShortener.Convert("");
		}


		[TestMethod]
		public void ConvertString_Negative()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(16, urlShortener.Base);
			var value = urlShortener.Convert("-7E");
			Trace.WriteLine(value);
			Assert.AreEqual(-0x7E, value.DecimalValue);
			Assert.AreEqual("-7E", value.BaseValue);
		}

		[TestMethod]
		public void ConvertString_Max()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(16, urlShortener.Base);
			var value = urlShortener.Convert(long.MaxValue);
			Trace.WriteLine(value);
		}

		[TestMethod]
		public void ConvertString_Confuse()
		{
			var urlShortener = new UrlShortener("9876543210");
			var value = urlShortener.Convert("123");
			Trace.WriteLine(value);
			Assert.AreEqual(876, value.DecimalValue);
		}


		[TestMethod]
		public void ConvertString_Base10()
		{
			var urlShortener = new UrlShortener(10);
			var value = urlShortener.Convert("123");
			Trace.WriteLine(value);
			Assert.AreEqual(123, value.DecimalValue);
		}

		[TestMethod]
		public void ConvertString_0()
		{
			var urlShortener = new UrlShortener("abBA");
			var value = urlShortener.Convert("a");
			Trace.WriteLine(value);
			Assert.AreEqual(0, value.DecimalValue);
		}

		[TestMethod]
		public void ConvertString_Unicode()
		{
			var urlShortener = new UrlShortener("☔☕☀☂♣♠☁");
			var value = urlShortener.Convert("☔☁☕");
			Trace.WriteLine(value);
			Assert.AreEqual(43, value.DecimalValue);

			value = urlShortener.Convert(long.MaxValue);
			Trace.WriteLine(value);
		}


		[ExpectedException(typeof(OverflowException))]
		[TestMethod]
		public void ConvertString_Overflow()
		{
			var urlShortener = new UrlShortener(Enumerable.Range(0, 254).Select(x => (char)x));
			Assert.AreEqual(254, urlShortener.Base);
			var value = urlShortener.Convert(" ");
			Assert.AreEqual(32, value.DecimalValue);
			Trace.WriteLine(value);
			urlShortener.Convert("Lucy in the sky with diamonds");
		}


		[TestMethod]
		public void ConvertString_Ascii()
		{
			var urlShortener = new UrlShortener(Enumerable.Range(0, 256).Select(x => (char)x));
			Assert.AreEqual(256, urlShortener.Base);
			var value = urlShortener.Convert("Nothing");
			Trace.WriteLine(value);
			Assert.AreEqual(22077593942060647, value.DecimalValue);


			value = urlShortener.Convert("Hello Dolly");
			Trace.WriteLine(value);
			Assert.AreEqual(7813499356810341497, value.DecimalValue);


			value = urlShortener.Convert(long.MaxValue);
			Trace.WriteLine(value);
		}

		[TestMethod]
		public void ConvertString_AlphaNumeric()
		{
			var list = new List<char>();
			list.AddRange(" abcdefghijklmnopqrstuvwxyz".ToUpperInvariant().ToCharArray());
			list.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());
			list.AddRange("0123456789".ToCharArray());


			var urlShortener = new UrlShortener(list);
			var value = urlShortener.Convert("Hello Dolly");
			Trace.WriteLine(value);
			Assert.AreEqual(8373672738368257056, value.DecimalValue);
		}


		[TestMethod]
		public void ConvertDecimal()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(16, urlShortener.Base);
			var value = urlShortener.Convert(0x7E);
			Trace.WriteLine(value);
			Assert.AreEqual(0x7E, value.DecimalValue);
			Assert.AreEqual("7E", value.BaseValue);
		}

		[TestMethod]
		public void ConvertDecimal_Binary()
		{
			var urlShortener = new UrlShortener("01");
			Assert.AreEqual(2, urlShortener.Base);
			var value = urlShortener.Convert(45);
			Trace.WriteLine(value);
			Assert.AreEqual(45, value.DecimalValue);
			Assert.AreEqual("101101", value.BaseValue);
		}


		[TestMethod]
		public void ConvertDecimal_0()
		{
			var urlShortener = new UrlShortener("XY");
			Assert.AreEqual(2, urlShortener.Base);
			var value = urlShortener.Convert(0);
			Trace.WriteLine(value);
			Assert.AreEqual(0, value.DecimalValue);
			Assert.AreEqual("X", value.BaseValue);
		}



		[TestMethod]
		public void ConvertDecimal_Negative()
		{
			var urlShortener = new UrlShortener("XY");
			Assert.AreEqual(2, urlShortener.Base);
			var value = urlShortener.Convert(-45);
			Trace.WriteLine(value);
			Assert.AreEqual(-45, value.DecimalValue);
			Assert.AreEqual("-YXYYXY", value.BaseValue);
		}


		[TestMethod]
		public void Convert_BackAndForth()
		{
			var chars = "AbBC";
			var urlShortener = new UrlShortener(chars);
			Assert.AreEqual(chars.Length, urlShortener.Base);


			long[] values = { 0, 992, 129, -126, 90909090, -90, -0, 11234, -11, 22, int.MaxValue * 10000000L, long.MaxValue};

			foreach (long i in values)
			{
				var value = urlShortener.Convert(i);
				var value2 = urlShortener.Convert(value.BaseValue);
				Trace.WriteLine($"{i}: {value} | {value2}");
				Assert.AreEqual(i, value2.DecimalValue);
			}
		}
		[TestMethod]
		public void ConvertString_MdSamples()
		{

			var urlShortener = new UrlShortener(CommonBase.Base62NumbersUpperLower);
			Trace.WriteLine(urlShortener.Base);
			Trace.WriteLine(urlShortener.Convert(long.MaxValue));
			Trace.WriteLine(urlShortener.Convert("Batman"));
		}

	}
}

