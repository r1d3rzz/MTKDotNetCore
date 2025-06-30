using Effortless.Net.Encryption;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MTKDotNetCore.AuthAPI.Helpers
{
    public class EncDecHelper
    {
        private readonly byte[] Key;
        private readonly byte[] VI;

        public EncDecHelper(IConfiguration configuration)
        {
            Key = Encoding.ASCII.GetBytes(configuration["Security:Key"]!);
            VI = Encoding.ASCII.GetBytes(configuration["Security:VI"]!);
        }

        public string EncString(string plainText)
        {
            return Strings.Encrypt(plainText, Key, VI);
        }

        public string DecString(string encText)
        {
            return Strings.Decrypt(encText, Key, VI);
        }
    }
}
