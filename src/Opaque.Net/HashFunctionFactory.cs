using System;
using Opaque.Net.Abstractions;
using Opaque.Net.HashFunctions;

namespace Opaque.Net
{
    public class HashFunctionFactory : IHashFunctionFactory
    {
        public IHashFunction Create(ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName, ICipherSuite cipherSuite)
            => cipherSuiteName switch
            {
                ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512 => new Sha512HashFunction(),
                // case ObliviousPseudoRandomFunctionCipherSuite.P256_SHA256:
                // case ObliviousPseudoRandomFunctionCipherSuite.P384_SHA256:
                // case ObliviousPseudoRandomFunctionCipherSuite.P521_SHA256:
                // case ObliviousPseudoRandomFunctionCipherSuite.Decaf448_SHA512:
                // case ObliviousPseudoRandomFunctionCipherSuite.None:
                _ => throw new NotSupportedException("Hash function for cipher suite " + cipherSuiteName.ToString()),
            };
    }
}
