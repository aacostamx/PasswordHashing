using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHashing
{
    class Program
    {
        public static void Main(string[] args)
        {
            var password = Encoding.ASCII.GetBytes("password");
            _passwordSalt = Encoding.ASCII.GetBytes("saltexample");
            _passwordHash = Hash(password, _passwordSalt);
            Console.WriteLine(Convert.ToBase64String(_passwordSalt));
            Console.WriteLine(ConfirmPassword("password"));

            Console.ReadKey();
        }

        private static byte[] _passwordSalt { get; set; }
        private static byte[] _passwordHash { get; set; }

        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();
            return new SHA256Managed().ComputeHash(saltedValue);
        }

        public static bool ConfirmPassword(string password)
        {
            byte[] passwordHash = Hash(password, _passwordSalt);

            return _passwordHash.SequenceEqual(passwordHash);
        }

    }
}
