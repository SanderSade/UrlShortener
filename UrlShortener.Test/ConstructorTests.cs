using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortUrl.Test
{
	[TestClass]
	[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
	public class ConstructorTests
	{
		[TestMethod]
		public void Construct()
		{
			var urlShortener = new UrlShortener("0123456789ABCDEF");
			Assert.AreEqual(16, urlShortener.Base);
		}


		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void Construct_Empty()
		{
			new UrlShortener("");
		}


		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void Construct_One()
		{
			new UrlShortener("1");
		}


		[ExpectedException(typeof(ApplicationException))]
		[TestMethod]
		public void Construct_Duplicates()
		{
			new UrlShortener("ABCcC");
		}


		[TestMethod]
		public void Construct_Ascii()
		{
			var urlShortener = new UrlShortener(Enumerable.Range(0, 254).Select(x => (char) x));
			Assert.AreEqual(254, urlShortener.Base);
		}


		[TestMethod]
		public void Construct_Radix()
		{
			var urlShortener = new UrlShortener(16);
			Assert.AreEqual(16, urlShortener.Base);

			urlShortener = new UrlShortener(2);
			Assert.AreEqual(2, urlShortener.Base);

			urlShortener = new UrlShortener(8);
			Assert.AreEqual(8, urlShortener.Base);
		}


		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void Constructor_Radix_Overflow()
		{
			new UrlShortener(128);
		}
	}
}
