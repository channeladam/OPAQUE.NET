using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public class ServerContext : ProtocolContext, IHasKeyPair
    {
        public ServerContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuite) : base(cipherSuite)
        {
            KeyPair = CryptoUtils.GenerateKeyPair(cipherSuite);
        }

        public KeyPair KeyPair { get; }
    }
}
