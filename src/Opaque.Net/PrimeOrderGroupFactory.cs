using System;
using Opaque.Net.Abstractions;
using Opaque.Net.PrimeOrderGroups;

namespace Opaque.Net
{
    public class PrimeOrderGroupFactory : IPrimeOrderGroupFactory
    {
        public IPrimeOrderGroup Create(ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName, ICipherSuite cipherSuite)
            => cipherSuiteName switch
            {
                ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512 => new Ristretto255PrimeOrderGroup(cipherSuite),
                // case ObliviousPseudoRandomFunctionCipherSuite.P256_SHA256:
                // case ObliviousPseudoRandomFunctionCipherSuite.P384_SHA256:
                // case ObliviousPseudoRandomFunctionCipherSuite.P521_SHA256:
                // case ObliviousPseudoRandomFunctionCipherSuite.Decaf448_SHA512:
                // case ObliviousPseudoRandomFunctionCipherSuite.None:
                _ => throw new NotSupportedException("Prime Order Group for cipher suite " + cipherSuiteName.ToString()),
            };
    }
}
