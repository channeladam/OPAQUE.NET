using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public class ClientContext : ProtocolContext, IHasKeyPair
    {
        public ClientContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuite) : base(cipherSuite)
        {
            KeyPair = CryptoUtils.GenerateKeyPair(cipherSuite);
        }

        public KeyPair KeyPair { get; }
    }
}
