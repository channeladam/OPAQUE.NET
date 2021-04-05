namespace Opaque.Net.Abstractions
{
    public interface IHashFunctionFactory
    {
        IHashFunction Create(ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName, ICipherSuite cipherSuite);
    }
}
