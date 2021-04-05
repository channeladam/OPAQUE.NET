using System;
using System.Collections.Generic;
using System.Linq;

namespace Opaque.Net.Internal
{
    public static class ByteArrayUtils
    {
        public static byte[] Concatenate(IList<byte[]> byteArrays)
        {
            if (byteArrays is null)
            {
                throw new ArgumentNullException(nameof(byteArrays));
            }

            if (byteArrays.Count == 0)
            {
                return Array.Empty<byte>();
            }

            int totalLength;
            try
            {
                totalLength = byteArrays.Sum(byteArray => byteArray.Length);
            }
            catch (ArithmeticException)
            {
                throw new ArgumentException("The total length of the byte arrays to be concatenated is too long");
            }

            byte[] result = new byte[totalLength];

            int targetIndex = 0;
            for (int index = 0; index < byteArrays.Count; index++)
            {
                byte[] currentArray = byteArrays[index];
                currentArray.CopyTo(result, targetIndex);
                targetIndex += currentArray.Length;
            }

            return result;
        }

        public static byte[] Xor(byte[] byteArray1, byte[] byteArray2)
        {
            if (byteArray1 is null)
            {
                throw new ArgumentNullException(nameof(byteArray1));
            }

            if (byteArray2 is null)
            {
                throw new ArgumentNullException(nameof(byteArray2));
            }

            if (byteArray1.Length != byteArray2.Length)
            {
                throw new ArgumentException("The byte array inputs must be of the same length");
            }

            int resultLength = byteArray1.Length;
            byte[] result = new byte[resultLength];

            for (int index = 0; index < resultLength; index++)
            {
                result[index] = (byte)(byteArray1[index] ^ byteArray2[index]);
            }

            return result;
        }
    }
}
