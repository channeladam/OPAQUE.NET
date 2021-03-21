using Opaque.Net.Abstractions;
// using Opaque.Net.Internal;

namespace Opaque.Net
{
    public class ClientContext : ProtocolContext //, IHasKeyPair
    {
        public ClientContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuite) : base(cipherSuite)
        {
            // TODO: is this needed?
            // KeyPair = CryptoUtils.GenerateKeyPair(cipherSuite);
        }

        // public KeyPair KeyPair { get; }
    }
}
