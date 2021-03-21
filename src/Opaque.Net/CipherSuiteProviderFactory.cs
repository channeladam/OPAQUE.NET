using System;
using Opaque.Net.HashFunctions;
using Opaque.Net.Internal;
using Opaque.Net.PrimeOrderGroups;

namespace Opaque.Net
{
    public sealed class CipherSuiteProviderFactory
    {
        private static readonly Lazy<bool> _lazyCryptoInitialisation = new(() =>
        {
            CryptoUtils.InitialiseCryptography();
            return true;
        });

        private static void EnsureCryptoIsInitialised()
        {
            bool _ = _lazyCryptoInitialisation.Value;
        }

        public static CipherSuite Create(ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName)
        {
            EnsureCryptoIsInitialised();

            return cipherSuiteName switch
            {
                ObliviousPseudoRandomFunctionCipherSuite.P256_SHA256 => ThrowNotSupportedCipherSuite(cipherSuiteName),
                ObliviousPseudoRandomFunctionCipherSuite.P384_SHA256 => ThrowNotSupportedCipherSuite(cipherSuiteName),
                ObliviousPseudoRandomFunctionCipherSuite.P521_SHA256 => ThrowNotSupportedCipherSuite(cipherSuiteName),
                ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512 => new CipherSuite(new Ristretto255PrimeOrderGroup(), new Sha512HashFunction()),
                ObliviousPseudoRandomFunctionCipherSuite.Decaf448_SHA512 => ThrowNotSupportedCipherSuite(cipherSuiteName),
                ObliviousPseudoRandomFunctionCipherSuite.None => ThrowNotSupportedCipherSuite(cipherSuiteName),
                _ => ThrowNotSupportedCipherSuite(cipherSuiteName)
            };
        }

        public static CipherSuite ThrowNotSupportedCipherSuite(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
            => throw new NotSupportedException("Cipher suite " + cipherSuite.ToString());
    }
}
