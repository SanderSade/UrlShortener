using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortUrl.Test
{
	[TestClass]
	public class PropertyTests
	{
		[TestMethod]
		public void Current()
		{
			var urlShortener = new UrlShortener("0123456789ABCDEF");
			Assert.AreEqual(0L, urlShortener.Current.DecimalValue);
			urlShortener = new UrlShortener("0123456789ABCDEF", 2143);
			Assert.AreEqual(2143L, urlShortener.Current.DecimalValue);
			urlShortener = new UrlShortener("0123456789ABCDEF", long.MinValue);
			Assert.AreEqual(long.MinValue, urlShortener.Current.DecimalValue);
		}


		[TestMethod]
		public void Next()
		{
			var urlShortener = new UrlShortener("0123456789ABCDEF");
			Assert.AreEqual(0L, urlShortener.Current.DecimalValue);
			Assert.AreEqual(1L, urlShortener.Next.DecimalValue);
			Assert.AreEqual(2L, urlShortener.Next.DecimalValue);
		}

		[TestMethod]
		public void Next_Negative()
		{

			var urlShortener = new UrlShortener("0123456789ABCDEF", int.MinValue);
			Assert.AreEqual(int.MinValue, urlShortener.Current.DecimalValue);
			Trace.WriteLine(urlShortener.Current);
			Assert.AreEqual(int.MinValue + 1L, urlShortener.Next.DecimalValue);
			Assert.AreEqual(int.MinValue + 1L, urlShortener.Current.DecimalValue);
			Trace.WriteLine(urlShortener.Current);
			Assert.AreEqual(int.MinValue + 2L, urlShortener.Next.DecimalValue);
			Trace.WriteLine(urlShortener.Current);
		}


		[TestMethod]
		public void Previous()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(0, urlShortener.Current.DecimalValue);
			Trace.WriteLine(urlShortener.Current);
			Assert.AreEqual(-1, urlShortener.Previous.DecimalValue);
			Trace.WriteLine(urlShortener.Current);
			Assert.AreEqual(-2, urlShortener.Previous.DecimalValue);
			Trace.WriteLine(urlShortener.Current);
		}


		[TestMethod]
		public void Base()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(16, urlShortener.Base);
			urlShortener = new UrlShortener("01");
			Assert.AreEqual(2, urlShortener.Base);
			urlShortener = new UrlShortener("acDC");
			Assert.AreEqual(4, urlShortener.Base);
		}


		[TestMethod]
		public void Characters()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual("0123456789ABCDEF", new string(urlShortener.Characters));
		}
	}
}
