using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public abstract class ProtocolContext : IProtocolContext
    {
        protected ProtocolContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
        {
            CipherSuite = cipherSuite;
        }

        public ObliviousPseudoRandomFunctionCipherSuite CipherSuite { get; }
    }
}
