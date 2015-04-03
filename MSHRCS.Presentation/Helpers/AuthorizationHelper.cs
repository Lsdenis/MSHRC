using System.Security.Cryptography;
using System.Text;

namespace MSHRCS.Presentation.Helpers
{
	public class AuthorizationHelper
	{
		public static string GetHashString(string password)
		{
			var bytes = Encoding.Unicode.GetBytes(password);
			var csp = new MD5CryptoServiceProvider();
			var byteHash = csp.ComputeHash(bytes);
			var hash = string.Empty;

			foreach (var b in byteHash)
			{
				hash += string.Format("{0:x2}", b);
			}
			return hash;
		}
	}
}