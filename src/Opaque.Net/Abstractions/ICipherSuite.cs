namespace Opaque.Net.Abstractions
{
    public interface ICipherSuite
    {
        ObliviousPseudoRandomFunctionCipherSuite CipherSuiteName { get; }
        ObliviousPseudoRandomFunctionProtocolMode ProtocolMode { get; }
        IPrimeOrderGroup PrimeOrderGroup { get; }
        IHashFunction HashFunction { get; }
        byte[] ProtocolContextString { get; }
    }
}
