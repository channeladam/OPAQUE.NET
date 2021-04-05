using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System // DO NOT CHANGE THIS FROM System!!!
{
    public static class ConversionExtensions
    {
        public static unsafe byte[] ConvertHexStringToByteArray(this string hex)
        {
            int byteLength = hex.Length >> 1;
            byte[] result = new byte[byteLength];
            ulong resultByteLength = 0;

            fixed (byte* resultPointer = &result[0])
            {
                Opaque.Net.Sodium.utils.SodiumHex2bin(resultPointer, (ulong)byteLength, hex, (ulong)hex.Length, null, ref resultByteLength, null);
            }

            return result;
        }

        public static byte[] ConvertStringAsUtf8ToByteArray(this string stringToConvert)
            => Encoding.UTF8.GetBytes(stringToConvert);

        public static string? ConvertToString(this SecureString secureString)
        {
            IntPtr stringPointer = IntPtr.Zero;

            try
            {
                stringPointer = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(stringPointer);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(stringPointer);
            }
        }

        public static byte[] ConvertToByteArray(this SecureString secureString)
        {
            IntPtr stringPointer = IntPtr.Zero;

            try
            {
                stringPointer = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                byte[] unicodeBytes = new byte[secureString.Length >> 1];
                for (int index = 0; index < unicodeBytes.Length; index++)
                {
                    unicodeBytes[index] = Marshal.ReadByte(stringPointer, index);
                }

                return unicodeBytes;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(stringPointer);
            }
        }
    }
}
