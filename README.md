# UrlShortener

## Introduction

The main purpose of the UrlShortener is to shorten in the URL.

Great many sites, such as [reddit](https://www.reddit.com), TinyUrl, bit.ly, goo.gl, t.co (Twitter) and Flickr, have a short alphanumeric ID (*[76pb94](https://www.reddit.com/r/programming/comments/76pb94/krack_attacks_breaking_wpa2/)*) instead of a long numeric value (*434521912*) in the URL. This makes the link shorter, easier for the user to see the differences in the URL - and it simply looks better, too.

There are two ways to achieve this. You can generate a string suitable for display and store it along with ID, or just store the numeric ID and convert the number to a string token and back. UrlShortener is intended to do the latter while giving more control to the developer than other similar libraries, as well as remaining easy to use and powerful.


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
Many URL shortening libraries either support just Int32 values (which a popular website will use up in a few months - or less), or even worse, only positive numbers. UrlShortener supports full Int64 range, from -9223372036854775808 to 9223372036854775807.
* **Full control of the used characters**  
Most URL shortening libraries allow to use just a fixed set of characters - usually a..z, or 0..z. UrlShortener not only has multiple predefined common sets, but also allows you to define any characters you like, in any order you like. You can do "012345" as your base or "123qwe" - both are properly handled as base 6. Or, for reverse decimal base, use "9876543210"
* **Performant**  
  * Converting 10 000 000 decimal items (starting from int.MaxValue, or 2 147 483 647) to base 36 (0..9, a..z) took 00:00:05.9824399, or about 1.7M operations per second.
  * Converting 10 000 000 base 36 items to decimal (using resulting values from previous test) took 00:00:02.6298898, or about 3.85M operations/s. This was with character validation disabled - with validation, it was 00:00:05.1318302, or roughly half as fast.  
The tests were done on a fairly low-powered laptop, and used sequential values, both of which affect the results - but they are a good indicator that performance really isn't an issue.
* **Includes common bases/encodings**  
UrlShortener includes more than 20 of common encodings/bases, including base 36, base64url (RFC4648), multiple variants of base 62, RFC3986-compliant base 65, base 12 (Unicode and non-Unicode versions) and many more - see [CommonBase.cs](https://github.com/SanderSade/UrlShortener/blob/master/UrlShortener/CommonBase.cs). This helps to use common URL or base conversions.  
* **Unicode support**  
I really don't recommend the use of Unicode for URL shortening, but should you want to use [Internationalized Resource Identifiers](https://www.w3.org/International/articles/idn-and-iri/) ([RFC3987](https://tools.ietf.org/html/rfc3987)) or use base 12 Unicode version, UrlShortener supports Unicode characters.
* **Sequence functionality**  
You can declare a starting decimal number in UrlShortener constructor (defaults to 0) and get sequential values in specified base calling `Next`. This is fully thread-safe, but the numbers are per UrlShortener instance (the library is intended to be used one-instance-per-base in your application/website).  
This should not be considered an alternative for database or other real sequence, but can be useful for unit or integration tests.  
UrlShortener also has `Current` and `Previous` properties, latter moves the current to previous value, e.g. Current becomes Current - 1.
* **.NET Standard 2.0**   
[.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md) means this library can be used with .NET Framework 4.6.1+, .NET Core 2.0 and more - see [here](https://github.com/dotnet/standard/blob/master/docs/versions.md) for detailed information.

## Examples

TBD

### Changelog
TBD
