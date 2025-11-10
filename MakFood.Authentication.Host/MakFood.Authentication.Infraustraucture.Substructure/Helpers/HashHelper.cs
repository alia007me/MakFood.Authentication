using System.Security.Cryptography;
using System.Text;

namespace MakFood.Authentication.Infraustraucture.Substructure.Helpers
{
    public static class HashHelper
    {
        public static string ComputeSha256(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be empty.", nameof(input));

            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha.ComputeHash(bytes);
            var builder = new StringBuilder();

            foreach (var b in hashBytes)
                builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
        public static string ComputeChecksum(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be empty.", nameof(input));
            var bytes = Encoding.UTF8.GetBytes(input);
            uint sum = 0;

            foreach (var b in bytes)
                sum = (sum + b) % 0xFFFFFFFF;
            return sum.ToString("X8");
        }

        public static bool VerifyChecksum(string input, string expectedChecksum)
        {
            if (expectedChecksum == null)
                throw new ArgumentNullException(nameof(expectedChecksum));

            var actual = ComputeChecksum(input);
            return string.Equals(actual, expectedChecksum, StringComparison.OrdinalIgnoreCase);
        }
    }
}
