# UrlShortener

## Introduction

The main purpose of the UrlShortener is to shorten IDs in your URL.

Great many sites, such as [reddit](https://www.reddit.com), TinyUrl, bit.ly, goo.gl, t.co (Twitter) and Flickr have a short alphanumeric ID (*[76pb94](https://www.reddit.com/r/programming/comments/76pb94/krack_attacks_breaking_wpa2/)*) instead of a long numeric value (*434521912*) in the URL. This makes the link shorter and also easier for user to see and differences in ID - and it simply looks better, too.

There are two ways to achieve this. You can generate a string suitable for display and store it along with your ID, or just store the numeric ID and convert the number to user-friendly token and back. UrlShortener is intended to do the latter while giving more control to the developer than other similar libraries while staying easy to use and powerful.


In addition, as a side-effect of the URL shortening functionality, UrlShortener can convert from **any [mathematical base](https://en.wikipedia.org/wiki/Radix) to decimal and back**.


##### Some examples

**Full English alphanumeric (0..9, A..Z, a..z) - or base 62 - range:**
* 9223372036854775807: AzL8n0Y58m7  
* 934556467467656: 4HNODorwe   

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


Note that 9223372036854775807 above is the maximum value that Int64 can hold, and also the maximum value supported by UrlShortener. Minimum value is long.MinValue, or -9223372036854775808.

## Features

* **Support for full Int64 range**  
Many other URL shortening libraries either support just Int32 values (which a popular website will use up in a few months), or even worse, only positive numbers. UrlShortener supports full Int64 range, from -9223372036854775808 to 9223372036854775807.
* **Full control of the used characters**  
Most other libraries allow to use just a fixed set of characters - usually a..z, or 0..z. UrlShortener not only has multiple predefined common sets, but also allows you to define any characters you like, in any order you like. You can do "1a2b3c" as your base or "123qwe" - both are properly handled, as base 6 but different values for characters
* **Performant**  
  * Converting 10 000 000 decimal items (starting from int.MaxValue, or 2 147 483 647) to base 36 (0..9, a..z) took 00:00:05.9824399, or 0.0000001 seconds per operation
  * Converting 10 000 000 base 36 items to decimal (using resulting values from previous test) took 00:00:02.6298898, or about 0.0000002 seconds per operation  
The tests were done on a fairly-low powered laptop, and used sequential values, both of which affect the results - but they are a good indicator that performance really isn't an issue.


## Examples

TBD

### Changelog
TBD
