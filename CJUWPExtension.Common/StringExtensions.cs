using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography.Core;

namespace CJUWPExtension.Common
{

    public static class StringExtensions
    {

        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        public static string MD5String(this string originalString, bool upperCase = false)
        {
            return originalString.MD5String(DefaultEncoding, upperCase);
        }

        public static string MD5String(this string originalString, Encoding encoding, bool upperCase = false)
        {
            return originalString == null ? null : string.Join(string.Empty, HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5).HashData((encoding ?? DefaultEncoding).GetBytes(originalString).AsBuffer()).ToArray().Select(@byte => string.Format(upperCase ? "{0:X2}" : "{0:x2}", @byte)));
        }

    }

}
