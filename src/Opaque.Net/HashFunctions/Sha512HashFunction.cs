using Opaque.Net.Abstractions;

namespace Opaque.Net.HashFunctions
{
    public class Sha512HashFunction : IHashFunction
    {
        public ushort InputBlockSizeInBytes => 128;
        public ushort OutputSizeInBits => 512;
        public ushort OutputSizeInBytes => 64;

        public byte[] HashData(byte[] source) => System.Security.Cryptography.SHA512.HashData(source);
    }
}
