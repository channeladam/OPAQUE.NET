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
    }
}
