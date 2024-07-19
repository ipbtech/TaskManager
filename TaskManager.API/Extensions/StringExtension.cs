using System.Security.Cryptography;
using System.Text;

namespace TaskManager.API.Extensions
{
    public static class StringExtension
    {
        public static string HashSha256(this string str)
        {
            string hash = string.Empty;
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }
    }
}
