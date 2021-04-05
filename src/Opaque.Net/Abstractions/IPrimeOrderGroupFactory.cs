namespace Opaque.Net.Abstractions
{
    public interface IPrimeOrderGroupFactory
    {
        IPrimeOrderGroup Create(ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName, ICipherSuite cipherSuite);
    }
}
