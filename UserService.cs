using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace son_atik_takip.Services
{
    public class UserService : IUserService
    {
        private readonly Dictionary<string, (string HashedPassword, string Role)> users = new Dictionary<string, (string, string)>
        {
            { "admin", (ComputeHash("admin123"), "Admin") },
            { "user", (ComputeHash("user123"), "User") }
        };

        public bool ValidateUser(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                string hashedInput = ComputeHash(password);
                return users[username].HashedPassword == hashedInput;
            }
            return false;
        }

        private static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
