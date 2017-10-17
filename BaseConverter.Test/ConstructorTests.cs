using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseConverter.Test
{
	[TestClass]
	public class ConstructorTests
	{
		[TestMethod]
		public void Construct()
		{
			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF");
			Assert.AreEqual(16, baseConverter.Base);
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void Construct_Empty()
		{
			new BaseConversion.BaseConverter("");
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void Construct_One()
		{
			new BaseConversion.BaseConverter("1");
		}


		[ExpectedException(typeof(ApplicationException))]
		[TestMethod]
		public void Construct_Duplicates()
		{
			new BaseConversion.BaseConverter("ABCcC");
		}

		[TestMethod]
		public void Construct_Ascii()
		{
			var baseConverter = new BaseConversion.BaseConverter(Enumerable.Range(0, 254).Select(x => (char)x));
			Assert.AreEqual(254, baseConverter.Base);
		}

	}
}
