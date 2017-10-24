using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortUrl.Test
{
	[TestClass]
	public class BaseConversionTests
	{
		private static BigInteger _lipsumDecimal = BigInteger.Parse("421796809724768874435444014194173719399258800141724988422308262736445256148563456642307106945098797131459239354561482987377947515026436852120536059062288131088016444215287454428911790680200041200426620368726487165411942859976333651252928086567080535940485449124843040439483417217637215496328060843929444070088736584579385716540415459221142368636954237139486545591588018651457363124043849973783567422664719064677055601008739460384430484277126966017743696409020585404229574953467296868666322631295950966516354278483437546246546910755635587490128551416370503544420063658138441322939788748908648705423454680777894937001837962830572221823401730396011229560859309350191836202032403242719687195778197240177876182490147933788440694139336332325519328979017999071472245956931060202754793592748310165801719506125463457178917307496998864521274470833123901564703339241691333990849598197727975816072859547416132341599447130379716252251070785022183319515720601606763365427667958061520263800185222183330663575845056062470386501698968807289145007973261246667775703378682321496448672979580016321318462893570208908124911841912305151999919861249748318515190600527562263679139728247283945875370945343730734196208143523500070416616197133510897021300710277676022799192946757156897320691352674929510056294761678561699358720461152797262466014246563748640888057830371248298491025714106830749874417064402341876195858395643418373391224604121417284161372799050180554551907689034654209797447568831129661151758655319322873786459634717129895910704261233639070756735919034930690374033708187260693078927179383389673128901123550148189656290040996782183516538917730137508412482089991650005223262454165449730238869598008763123250153091166198414274627647278954678424107173707168448425877463677854927978597000860073658574554350647392946739951576455549238923599296575229475673360248606184409311476982542609483189261580508916128576652743096607269552178016224372667636483993624909913748495168375021813975400251311280210387747596314831815645099315360194629766875953281586585566208762332329334165019236791761722536847479029894324830138883279619754656520780806943235897985601797970168889046062");


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



		[TestMethod]
		public void ConvertString_Ascii()
		{
			var urlShortener = new UrlShortener(Enumerable.Range(0, 256).Select(x => (char) x));
			Assert.AreEqual(256, urlShortener.Base);
			var value = urlShortener.Convert("Nothing");
			Trace.WriteLine(value);
			Assert.AreEqual(22077593942060647, value.DecimalValue);

			var lipsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac sem enim. Vivamus hendrerit semper eros, tincidunt condimentum est pulvinar porttitor. Ut eu diam sagittis, tincidunt velit a, maximus eros. Morbi justo nisi, hendrerit id fringilla ac, finibus vestibulum purus. Nam aliquet lectus quis erat fermentum sodales. Donec in velit pellentesque, sodales ligula et, egestas ipsum. Aliquam erat volutpat. Phasellus lacinia efficitur libero, et sodales augue sollicitudin in. Etiam efficitur eleifend quam, et vestibulum tellus tincidunt eu. Duis sed odio pellentesque, blandit augue sit amet, feugiat leo. Vivamus malesuada tortor in sapien tristique aliquet. Maecenas lacus lorem, commodo ac sagittis id, suscipit convallis mauris. Nam sodales dolor at leo lobortis dignissim. Fusce sollicitudin consectetur quam in mollis. Proin pretium vulputate fringilla. Maecenas vel varius erat.";
			value = urlShortener.Convert(lipsum);
			Trace.WriteLine(value);
			Assert.AreEqual(_lipsumDecimal, value.DecimalValue);

			value = urlShortener.Convert(_lipsumDecimal);
			Assert.AreEqual(lipsum, value.BaseValue);

			value = urlShortener.Convert(long.MaxValue);
			Trace.WriteLine(value);
		}



		[TestMethod]
		public void ConvertString_UnicodeBig()
		{
			const int characters = 55000;
			var urlShortener = new UrlShortener(Enumerable.Range(0, characters).Select(x => (char)x));
			Assert.AreEqual(characters, urlShortener.Base);

			var value = urlShortener.Convert(_lipsumDecimal);
			Trace.WriteLine(value);

			var revert = urlShortener.Convert(value.BaseValue);

			Assert.AreEqual(_lipsumDecimal, revert.DecimalValue);

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

			BigInteger[] values = { 0, 992, 129, -126, 90909090, -90, -0, 11234, -11, 22, int.MaxValue * 10000000L, long.MaxValue, BigInteger.Multiply(long.MaxValue, long.MaxValue), _lipsumDecimal, -_lipsumDecimal };

			foreach (var i in values)
			{
				var value = urlShortener.Convert(i);
				var value2 = urlShortener.Convert(value.BaseValue);
				Trace.WriteLine($"{i}: {value} | {value2}");
				Assert.AreEqual(i, value2.DecimalValue);
			}
		}
	}
}
