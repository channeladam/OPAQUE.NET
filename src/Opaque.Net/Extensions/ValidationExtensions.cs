using System.Globalization;
using System.Security.Cryptography;

namespace System // DO NOT CHANGE THIS FROM System!!
{
    public static class ValidationExtensions
    {
        public static void ValidateLength(this byte[] array, int expectedLength, string errorName)
            => ValidateLength(array.Length, expectedLength, errorName);

        public static void ValidateLength(this byte[] array, uint expectedLength, string errorName)
            => ValidateLength(array.Length, expectedLength, errorName);

        public static void ValidateLength(this ReadOnlySpan<byte> array, int expectedLength, string errorName)
            => ValidateLength(array.Length, expectedLength, errorName);

        public static void ValidateLength(this ReadOnlySpan<byte> array, uint expectedLength, string errorName)
            => ValidateLength(array.Length, expectedLength, errorName);

        private static void ValidateLength(int actualLength, int expectedLength, string errorName)
        {
            if (actualLength == expectedLength)
            {
                return;
            }

            throw new CryptographicException($"Array '{errorName}' has length '{actualLength.ToString(CultureInfo.InvariantCulture)}' but was expected to be length '{expectedLength.ToString(CultureInfo.InvariantCulture)}'");
        }

        private static void ValidateLength(int actualLength, uint expectedLength, string errorName)
        {
            if (actualLength == expectedLength)
            {
                return;
            }

            throw new CryptographicException($"Array '{errorName}' has length '{actualLength.ToString(CultureInfo.InvariantCulture)}' but was expected to be length '{expectedLength.ToString(CultureInfo.InvariantCulture)}'");
        }
    }
}
