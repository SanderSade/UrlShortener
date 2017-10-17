# BaseConverter

## Introduction

The purpose of the BaseConverter is twofold:

1. Convert from any [mathematical base](https://en.wikipedia.org/wiki/Radix) to decimal and back
2. Main intended use of the BaseConverter is to have shorter ID's in your URL's. 

	E.g. many sites, such as [reddit](https://www.reddit.com), have a short alphanumeric ID (*[76pb94](https://www.reddit.com/r/programming/comments/76pb94/krack_attacks_breaking_wpa2/)*) instead of long numeric value (*434521912*) in the URL. This makes it easier for user to see and/or link differences in ID, as well as that simply looks better.

	There are two ways to achieve this. You can generate a string suitable for display and store it along with your ID, or just store the numeric ID and convert the number to user-friendly token and back. BaseConverter is intended to do the latter, with maximum freedom for the developer.

###### Some examples

**Full English alphanumeric (0..9, A..Z, a..z) - or base 62 - range:**
* 9223372036854775807: AzL8n0Y58m7  
* 934556467467656: 4HNODorwe   

**Base 36 (0..9, a..z), like reddit uses:**
* 434521912: 76pb94
* 9223372036854775807: 1y2p0ij32e8e7

**[Extended ASCII](https://en.wikipedia.org/wiki/Extended_ASCII) (base 256 - note that this is encoding-specific!):**
* 22077593942060647: Nothing
* 7813499356810341497: Hello Dolly
* 292756923184539821: ÞÛóh­  
* 9223372036854775807: ÿÿÿÿÿÿÿ  


Note that 9223372036854775807 above is maximum value that Int64 can hold, and also the maximum value supported by BaseConverter. Minimum value is long.MinValue, or -9223372036854775808.

## Features

TBD


## Examples

TBD

### Changelog
TBD
