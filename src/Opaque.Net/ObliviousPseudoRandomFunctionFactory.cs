using System;
using System.Security.Cryptography;

namespace Opaque.Net
{
    public static class ObliviousPseudoRandomFunctionFactory
    {
        public static ECCurve CreateECCurve(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
            => cipherSuite switch
            {
                // TODO: merge into CipherSuiteProviderFactory
                ObliviousPseudoRandomFunctionCipherSuite.P256_SHA256 => ECCurve.NamedCurves.nistP256,
                ObliviousPseudoRandomFunctionCipherSuite.P384_SHA256 => ECCurve.NamedCurves.nistP384,
                ObliviousPseudoRandomFunctionCipherSuite.P521_SHA256 => ECCurve.NamedCurves.nistP521,
                ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512 => ThrowNotSupportedCipherSuite(cipherSuite),
                ObliviousPseudoRandomFunctionCipherSuite.Decaf448_SHA512 => ThrowNotSupportedCipherSuite(cipherSuite),
                ObliviousPseudoRandomFunctionCipherSuite.None => ThrowNotSupportedCipherSuite(cipherSuite),
                _ => ThrowNotSupportedCipherSuite(cipherSuite)
            };

        public static ECCurve ThrowNotSupportedCipherSuite(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
            => throw new NotSupportedException("Cipher suite " + cipherSuite.ToString());
    }
}
