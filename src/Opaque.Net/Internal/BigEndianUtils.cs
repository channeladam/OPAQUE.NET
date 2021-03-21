using System;
using System.Numerics;

namespace Opaque.Net.Internal
{
    internal static class BigEndianUtils
    {
        /// <summary>
        /// Convert an unsigned integer to the big-endian ordinal string primitive (I2OSP)
        /// as per https://datatracker.ietf.org/doc/html/rfc8017#section-4.1.
        /// </summary>
        /// <param name="value">The unsigned integer to convert.</param>
        /// <param name="outputLength">Intended length of the resulting octet string.</param>
        internal static Span<byte> ConvertIntegerToOrdinalStringPrimitive(uint value, ushort outputLength)
        {
            if (value >= Math.Pow(256, outputLength))
            {
                throw new ArithmeticException("Integer is too large for the intended output length");
            }

            byte[] result = new byte[outputLength];
            for (int index = outputLength - 1; index >= 0; index--)
            {
                result[index] = (byte)(value >> (8 * (outputLength - 1 - index)));
            }

            return result;
        }

        /// <summary>
        /// Convert an unsigned integer to the big-endian ordinal string primitive (I2OSP)
        /// as per https://datatracker.ietf.org/doc/html/rfc8017#section-4.1.
        /// </summary>
        /// <param name="value">The unsigned integer to convert.</param>
        /// <param name="outputLength">Intended length of the resulting octet string.</param>
        internal static void ConvertIntegerToOrdinalStringPrimitive(
            uint value,
            ushort outputLength,
            ref byte[] outputByteArray,
            uint outputArrayOffset)
        {
            if (value >= Math.Pow(256, outputLength))
            {
                throw new ArithmeticException("Integer is too large for the intended output length");
            }

            if (outputByteArray is null)
            {
                throw new ArgumentNullException(nameof(outputByteArray));
            }

            if ((outputArrayOffset + outputLength) > outputByteArray.Length)
            {
                throw new ArithmeticException("Integer is too large for the intended output length with the given output byte array and offset");
            }

            for (int index = outputLength - 1; index >= 0; index--)
            {
                outputByteArray[outputArrayOffset + index] = (byte)(value >> (8 * (outputLength - 1 - index)));
            }
        }

        /// <summary>
        /// Convert a big-endian ordinal string primitive to an unsigned integer (OS2IP)
        /// as per https://datatracker.ietf.org/doc/html/rfc8017#section-4.2.
        /// </summary>
        internal static uint ConvertOrdinalStringToIntegerPrimitive(byte[] input)
        {
            if (input.Length > 4)
            {
                throw new ArithmeticException("Byte array is too large for an integer to be returned");
            }

            if (input.Length == 0)
            {
                return 0;
            }

            // uint result = 0;
            // for (int index = 0; index < input.Length; index++)
            // {
            //     result |= (uint)(input[index] & 0xff) << (8 * (input.Length - 1 - index));
            // }
            // return result;

            BigInteger big = new BigInteger(input, isUnsigned: true, isBigEndian: true);
            return (uint)big;
        }
    }
}
