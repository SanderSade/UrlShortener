using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShortUrl.Test
{
	/// <summary>
	/// Rudimentary speed tests. Speed is not really an issue, so I am not bothering with better ones
	/// todo: move testing functionality to a client console app...
	/// </summary>
	[TestClass]
	public class SpeedTests
	{
		private List<string> _stringCache;
		private List<long> _longCache;


		[TestMethod]
		public void DecimalToBase36()
		{
			const long iterations = 10000000L;
			var urlShortener = new UrlShortener(CommonEncoding.Base95);
			_stringCache = new List<string>((int) iterations);
			Thread.Sleep(100);

			var sw = Stopwatch.StartNew();

			for (long i = int.MaxValue; i < int.MaxValue + iterations; i++)
			{
				_stringCache.Add(urlShortener.Convert(i).BaseValue);
			}

			sw.Stop();

			Trace.WriteLine($"Converting {_stringCache.Count} took {sw.Elapsed}, or {new TimeSpan(sw.ElapsedTicks / iterations)} per operation");
		}


		[TestMethod]
		public void Base36ToDecimal()
		{
			//fill the list
			DecimalToBase36();
			var urlShortener = new UrlShortener(CommonEncoding.Base95);

			_longCache = new List<long>(_stringCache.Count);
			Thread.Sleep(100);

			var sw = Stopwatch.StartNew();
			foreach (var value in _stringCache)
			{
				// ReSharper disable once PossibleInvalidOperationException
				_longCache.Add(urlShortener.Convert(value).DecimalValue.Value);
			}
			sw.Stop();

			Trace.WriteLine($"Converting {_longCache.Count} to decimal took {sw.Elapsed}, or {new TimeSpan(sw.ElapsedTicks / _longCache.Count)} per operation");

		}
	}
}
