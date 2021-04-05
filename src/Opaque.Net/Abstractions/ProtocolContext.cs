namespace Opaque.Net.Abstractions
{
    public abstract class ProtocolContext : IProtocolContext
    {
        protected ProtocolContext(CipherSuite cipherSuite)
        {
            CipherSuite = cipherSuite;
        }

        public CipherSuite CipherSuite { get; }
    }
}
