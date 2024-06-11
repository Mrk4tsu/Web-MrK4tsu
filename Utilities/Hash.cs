using Konscious.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public class Hash
    {
        private static Hash Instance;
        public static Hash GetInstance()
        {
            if (Instance == null) Instance = new Hash();
            return Instance;
        }
        public string HashPassword(string password)
        {
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                hasher.Salt = new byte[16];
                hasher.DegreeOfParallelism = 8;
                hasher.MemorySize = 65536;
                hasher.Iterations = 4;

                var hashBytes = hasher.GetBytes(128);
                return Convert.ToBase64String(hashBytes); // Convert byte[] to base64 string
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var newHash = HashPassword(password);
            return newHash.Equals(hashedPassword);
        }
        public string SHA256Password(string password)
        { 
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
