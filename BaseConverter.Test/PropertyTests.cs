using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseConverter.Test
{
	[TestClass]
	public class PropertyTests
	{
		[TestMethod]
		public void Current()
		{
			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF");
			Assert.AreEqual(0L, baseConverter.Current.DecimalValue);
			baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF", 2143);
			Assert.AreEqual(2143L, baseConverter.Current.DecimalValue);
			baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF", long.MinValue);
			Assert.AreEqual(long.MinValue, baseConverter.Current.DecimalValue);
		}


		[TestMethod]
		public void Next()
		{
			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF");
			Assert.AreEqual(0L, baseConverter.Current.DecimalValue);
			Assert.AreEqual(1L, baseConverter.Next.DecimalValue);
			Assert.AreEqual(2L, baseConverter.Next.DecimalValue);
		}

		[TestMethod]
		public void Next_Negative()
		{

			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF", int.MinValue);
			Assert.AreEqual(int.MinValue, baseConverter.Current.DecimalValue);
			Trace.WriteLine(baseConverter.Current);
			Assert.AreEqual(int.MinValue + 1L, baseConverter.Next.DecimalValue);
			Assert.AreEqual(int.MinValue + 1L, baseConverter.Current.DecimalValue);
			Trace.WriteLine(baseConverter.Current);
			Assert.AreEqual(int.MinValue + 2L, baseConverter.Next.DecimalValue);
			Trace.WriteLine(baseConverter.Current);
		}


		[TestMethod]
		public void Previous()
		{
			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF");
			Assert.AreEqual(0, baseConverter.Current.DecimalValue);
			Trace.WriteLine(baseConverter.Current);
			Assert.AreEqual(-1, baseConverter.Previous.DecimalValue);
			Trace.WriteLine(baseConverter.Current);
			Assert.AreEqual(-2, baseConverter.Previous.DecimalValue);
			Trace.WriteLine(baseConverter.Current);
		}


		[TestMethod]
		public void Base()
		{
			var baseConverter = new BaseConversion.BaseConverter("0123456789ABCDEF");
			Assert.AreEqual(16, baseConverter.Base);
			baseConverter = new BaseConversion.BaseConverter("01");
			Assert.AreEqual(2, baseConverter.Base);
			baseConverter = new BaseConversion.BaseConverter("acDC");
			Assert.AreEqual(4, baseConverter.Base);
		}
	}
}