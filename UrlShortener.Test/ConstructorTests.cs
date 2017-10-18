using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortUrl.Test
{
	[TestClass]
	public class ConstructorTests
	{
		[TestMethod]
		public void Construct()
		{
			var baseConverter = new UrlShortener("0123456789ABCDEF");
			Assert.AreEqual(16, baseConverter.Base);
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
			var baseConverter = new UrlShortener(Enumerable.Range(0, 254).Select(x => (char) x));
			Assert.AreEqual(254, baseConverter.Base);
		}


		[TestMethod]
		public void Construct_Radix()
		{
			var baseConverter = new UrlShortener(16);
			Assert.AreEqual(16, baseConverter.Base);

			baseConverter = new UrlShortener(2);
			Assert.AreEqual(2, baseConverter.Base);

			baseConverter = new UrlShortener(8);
			Assert.AreEqual(8, baseConverter.Base);
		}
	}
}
