using System.Security.Cryptography;
using Opaque.Net.Abstractions;

namespace Opaque.Net.HashFunctions
{
    public class Sha512HashFunction : IHashFunction
    {
        private static readonly SHA512Managed _hashFunction = new();

        public ushort InputBlockSizeInBytes => 128;
        public ushort OutputSizeInBits => 512;
        public ushort OutputSizeInBytes => 64;

        public byte[] HashData(byte[] source) => _hashFunction.ComputeHash(source);
    }
}
