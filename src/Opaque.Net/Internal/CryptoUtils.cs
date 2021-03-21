using System.Security.Cryptography;

namespace Opaque.Net.Internal
{
    internal static class CryptoUtils
    {
        internal static void InitialiseCryptography()
        {
            // sodium_init() returns 0 on success, -1 on failure, and 1 if the library had already been initialized.
            if (Sodium.core.SodiumInit() != -1)
            {
                return;
            }

            throw new CryptographicException("Unable to initialise libsodium cryptographic library");
        }

        internal static string GetSodiumLibraryVersion()
            => Sodium.version.SodiumVersionString();

        // // TODO: move into PrimeOrderGroup impl
        // internal static KeyPair GenerateKeyPair(IProtocolContext protocolContext)
        //     => GenerateKeyPair(protocolContext.CipherSuiteName);

        // // TODO: move into PrimeOrderGroup impl
        // internal static KeyPair GenerateKeyPair(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
        // {
        //     ECCurve curve = ObliviousPseudoRandomFunctionFactory.CreateECCurve(cipherSuite);
        //     using ECDsa digitalSignatureAlgorithm = ECDsa.Create(curve);
        //     ECParameters ecParams = digitalSignatureAlgorithm.ExportParameters(includePrivateParameters: true);
        //     return new KeyPair(ecParams);
        // }

        // // internal static ECCurve CreateExplicitCurve(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
        // // {
        // //     ECCurve curve = ObliviousPseudoRandomFunctionFactory.CreateECCurve(cipherSuite);
        // //     using ECDsa digitalSignatureAlgorithm = ECDsa.Create(curve);
        // //     ECParameters ecParams = digitalSignatureAlgorithm.ExportExplicitParameters(includePrivateParameters: true);
        // //     return ecParams.Curve;
        // // }
    }
}
