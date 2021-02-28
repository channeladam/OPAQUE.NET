using System.Security.Cryptography;
using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public static class CryptoUtils
    {
        public static KeyPair GenerateKeyPair(IProtocolContext protocolContext)
            => GenerateKeyPair(protocolContext.CipherSuite);

        public static KeyPair GenerateKeyPair(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
        {
            ECCurve curve = ObliviousPseudoRandomFunctionFactory.CreateECCurve(cipherSuite);
            using ECDsa digitalSignatureAlgorithm = ECDsa.Create(curve);
            ECParameters ecParams = digitalSignatureAlgorithm.ExportParameters(includePrivateParameters: true);
            return new KeyPair(ecParams);
        }
    }
}
