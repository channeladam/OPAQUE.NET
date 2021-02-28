using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public class ServerContext : ProtocolContext, IHasKeyPair
    {
        public ServerContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuite) : base(cipherSuite)
        {
            // TODO - get this from a secret store that only the server has access to
            KeyPair = CryptoUtils.GenerateKeyPair(cipherSuite);
        }

        public KeyPair KeyPair { get; }
    }
}
