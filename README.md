[![GitHub license](https://img.shields.io/badge/licence-MPL%202.0-brightgreen.svg)](https://github.com/SanderSade/UrlShortener/blob/master/LICENSE)
[![NetStandard 2.0](https://img.shields.io/badge/-.NET%20Standard%202.0-green.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)
[![NuGet v1.0.0](https://img.shields.io/badge/NuGet-v1.0.0-lightgrey.svg)](https://www.nuget.org/packages/SanderSade.UrlShortener/)

## Introduction

The main purpose of the UrlShortener is to shorten in the URLs

Great many sites, such as [reddit](https://www.reddit.com), TinyUrl, bit.ly, goo.gl, t.co (Twitter) and Flickr, have a short alphanumeric ID (*[76pb94](https://www.reddit.com/r/programming/comments/76pb94/krack_attacks_breaking_wpa2/)*) instead of a long numeric value (*434521912*) in the URL. This makes the link shorter, easier for the user to see the differences in the URL - and it simply looks better, too.

There are two ways to achieve this. You can generate a string suitable for display and store it along with ID, or just store the numeric ID and convert the number to a string token and back. UrlShortener is intended to do the latter while giving more control to the developer than other similar libraries.


In addition, as a side-effect of the URL shortening functionality, UrlShortener can convert from **any positional [mathematical base](https://en.wikipedia.org/wiki/Radix) system to decimal and back**.


##### Some examples

**Full English alphanumeric (0..9, A..Z, a..z) - or base 62 - range:**
* 9223372036854775807: AzL8n0Y58m7  
* 934556467467656: 4HNODorwe   
* 10622704081: Batman

**Base 36 (0..9, a..z), like reddit uses:**
* 434521912: 76pb94
* 9223372036854775807: 1y2p0ij32e8e7

**[Extended ASCII](https://en.wikipedia.org/wiki/Extended_ASCII) (base 256 - note that this is encoding-specific, and includes unprintable control characters!):**
* 22077593942060647: Nothing
* 7813499356810341497: Hello Dolly
* 292756923184539821: ÞÛóh­  
* 9223372036854775807: ÿÿÿÿÿÿÿ  


**Unicode (using symbols ☔☕☀☂♣♠☁):** 
* 43: ☔☁☕  
* 9223372036854775807: ☀☀☂♣☕☔☕☔☁☕☕☀♣♠☔♠☀☔♠☀☂☔☔ 


## Features

* **Support for full Int64 range**  
Many URL shortening libraries either support just Int32 values (which a popular website will use up in a few months - or less), or just positive numbers. UrlShortener supports full Int64 range, from -9223372036854775808 to 9223372036854775807.
* **Full control of the used characters**  
Most URL shortening libraries allow to use just a fixed set of characters - usually a..z, or 0..z. UrlShortener not only has multiple predefined common character sets, but also lets you to define any characters you want, in any order you want. You can do "012345" as your base or "123qwe" - both are properly handled as base 6. Or, for example, use the reversed decimal base, "9876543210".
* **Includes common bases/character sets**  
UrlShortener includes more than 20 of common character sets/bases, including base 36, base64url (RFC4648), multiple variants of base 62, RFC3986-compliant base 65, base 12 (Unicode and non-Unicode versions) and many more - see [CharacterSet.cs](https://github.com/SanderSade/UrlShortener/blob/master/UrlShortener/CharacterSet.cs).  
* **Unicode support**  
I really don't recommend the use of Unicode for URL shortening, but should you want to use [Internationalized Resource Identifiers](https://www.w3.org/International/articles/idn-and-iri/) ([RFC3987](https://tools.ietf.org/html/rfc3987)) or use base 12 Unicode version, UrlShortener supports Unicode characters.
* **Input validation**  
There are some libraries that don't validate characters when converting to decimal. Depending on the algorithm used, this may return an invalid base-10 value without any errors. UrlShortener validates the input against your defined character set, and throws ApplicationException() if it contains invalid characters.   
* **Fast**  
  * Converting 10 000 000 decimal items (starting from int.MaxValue, or 2 147 483 647) to base 36 (0..9, a..z) took 00:00:05.9824399, or about 1.7M operations per second.
  * Converting 10 000 000 base 36 items to decimal (using resulting values from previous test) took 00:00:05.1318302.  
The tests were done on a not-all-that-powerful laptop, and used sequential values, both of which affect the results - but they are a good indicator that performance really isn't something to worry about.  
* **Sequence functionality**  
You can declare a starting decimal number in UrlShortener constructor (defaults to 0) and get sequential values in specified base calling `Next`. This is fully thread-safe, but the numbers are per UrlShortener instance (the library is intended to be used one-instance-per-base in your application/website).  
This should not be considered an alternative for database or other real sequence, but can be useful for unit or integration tests.  
UrlShortener also has `Current` and `Previous` properties, latter moves the current to previous value, e.g. Current becomes Current - 1.
* **.NET Standard 2.0**   
[.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md) means this library can be used with .NET Framework 4.6.1+, .NET Core 2.0 and more - see [here](https://github.com/dotnet/standard/blob/master/docs/versions.md) for detailed information.

### Help & examples


As said before, you should have just one instance per base in your application - e.g. initiate UrlShortener in your startup or first use, and store the instance in a static variable - or configure your dependency injection accordingly.

A lot of examples can be found in the unit test project - [BaseConversionTests.cs](https://github.com/SanderSade/UrlShortener/blob/master/UrlShortener.Test/BaseConversionTests.cs).  

Download UrlShortener from [Releases](https://github.com/SanderSade/UrlShortener/releases) or fetch it via [NuGet](https://www.nuget.org/packages/SanderSade.UrlShortener/).

* Simple conversion to base 62 (0..9, A..Z, a..z version), using .Convert() overloads:
```
var urlShortener = new UrlShortener(CharacterSet.Base62NumbersUpperLower);
var decimalValue = urlShortener.Convert("fgzY7zdiN");
Console.WriteLine(decimalValue); //9103348223453411: fgzY7zdiN
Console.WriteLine(decimalValue.DecimalValue); //9103348223453411
var baseValue = urlShortener.Convert(90909090);
Console.WriteLine(baseValue); //90909090: 69Rbe
Console.WriteLine(baseValue.BaseValue); //69Rbe
```

* Base 24 conversions:
```
var urlShortener = new UrlShortener(24);
Console.WriteLine(urlShortener.FromInt64(986125456798543667)); //13014J3141FNFB
Console.WriteLine(urlShortener.ToInt64("1A2B3C4D5E6F"));//2162225485250079
``` 

* Binary conversion using custom symbols:
```
var urlShortener = new UrlShortener("dT");
Console.WriteLine(urlShortener.FromInt64(1398612542256797)); //TddTTTTTddddddddTTTTTdTTdTTTTddddTddTTTdTTdTddTTTdT
Console.WriteLine(urlShortener.ToInt64("ddTTdd")); //12
```

* Next/Previous/Current functionality:
```
var urlShortener = new UrlShortener("Uncopyrightable", 1000); //longest word in English without repeating characters
Console.WriteLine(urlShortener.Current); //1000: prt
Console.WriteLine(urlShortener.Next); //1001: pra
Console.WriteLine(urlShortener.Current); //1001: pra
Console.WriteLine(urlShortener.Previous); //1000: prt
Console.WriteLine(urlShortener.Previous); //999: prh
Console.WriteLine(urlShortener.Current); //999: prh
```

### Changelog
* 1.0 Initial release

### Future plans & ideas
* Think about switching from Int64 to BigInteger - this would allow converting GUID to other bases - but would use more resources.  
* Add a simple MVC project demonstrating how to use the library
