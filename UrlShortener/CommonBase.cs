using System.Diagnostics.CodeAnalysis;

namespace ShortUrl
{
	/// <summary>
	/// Commonly used character sets
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public static class CommonBase
	{

		/// <summary>
		/// English alphabet, a..z. Lowercase
		/// </summary>
		public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// Alphanumeric, 0..9, A..Z
		/// </summary>
		public const string Base36Uppercase = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Alphanumeric, 0..9, a..z
		/// </summary>
		public const string Base36Lowercase = "0123456789abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// Alphanumeric, 0..9, a..z, A..Z
		/// </summary>
		public const string Base62NumbersLowerUpper = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Alphanumeric, 0..9, A..Z, a..z
		/// </summary>
		public const string Base62NumbersUpperLower = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// Alphanumeric, a..z, A..Z, 0..9
		/// </summary>
		public const string Base62LowerUpperNumbers = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		/// <summary>
		/// Alphanumeric, A..Z, a..z, 0..9
		/// </summary>
		public const string Base62UpperLowerNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		/// <summary>
		/// Hexadecimal, 0..F
		/// </summary>
		public const string Hexadecimal = "0123456789ABCDEF";

		/// <summary>
		/// All unreserved characters allowed per RFC3986, excluding tilde ("~"), which is commonly used to denote user catalog and such
		/// <para>Base65. This is the highest base that does not need special encoding for URL</para>
		/// <para>See https://tools.ietf.org/html/rfc3986#section-2.3 </para>
		/// <para>and also the mess that is https://www.w3.org/TR/html5/forms.html#application/x-www-form-urlencoded-encoding-algorithm </para>
		/// </summary>
		public const string Base65 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._";

		/// <summary>
		/// Same as RFC3986/Base65, but excludes .
		/// See https://tools.ietf.org/html/rfc4648#section-5
		/// </summary>
		public const string Base64Url = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

		/// <summary>
		/// Base58, as used by Bitcoin
		/// <para>https://en.wikipedia.org/wiki/Base58</para>
		/// </summary>
		public const string Base58Bitcoin = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

		/// <summary>
		/// Base58, as used by Flickr short URLs
		/// <para>https://en.wikipedia.org/wiki/Base58</para>
		/// </summary>
		public const string Base58Flickr = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";

		/// <summary>
		/// See https://tools.ietf.org/html/rfc4648
		/// </summary>
		public const string Base32 = "ABCDEFGHIJKLMNOPQRSTUBWXYZ234567";

		/// <summary>
		/// Base8 aka octal, 0..7
		/// </summary>
		public const string Base8 = "01234567";

		/// <summary>
		/// All printable ASCII characters, excluding space
		/// </summary>
		public const string Base94 = "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

		/// <summary>
		/// All printable ASCII characters, including space
		/// </summary>
		public const string Base95 = " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

		/// <summary>
		/// ASCII 85, per https://tools.ietf.org/html/rfc1924
		/// </summary>
		public const string Ascii85 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!#$%&()*+-;<=>?@^_`{|}~";

		/// <summary>
		/// Base 20, or vigesimal. This is the variant with IJ as last characters, not JK
		/// <para>See https://en.wikipedia.org/wiki/Vigesimal </para>
		/// </summary>
		public const string Base20 = "0123456789ABCDEFGHIJ";

		/// <summary>
		/// Base 12 or duodecimal.
		/// <para>This is the computer science version, 0..B</para>
		/// <para>https://en.wikipedia.org/wiki/Duodecimal</para>
		/// </summary>
		public const string Base12Standard = "0123456789AB";


		/// <summary>
		/// Base 12 or duodecimal.
		/// <para>This is the "British" version, which uses rotated 2 (↊) and 3 (↋) for last two characters</para>
		/// <para>https://en.wikipedia.org/wiki/Duodecimal</para>
		/// </summary>
		public const string Base12Unicode = "0123456789↊↋";

		/// <summary>
		/// Base 60 or sexagesimal (0..9, A..Z, a..x)
		/// <para>https://en.wikipedia.org/wiki/Sexagesimal</para>
		/// </summary>
		public const string Base60 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx";
	}
}
