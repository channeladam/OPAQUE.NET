using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public class CipherSuite
    {
        public CipherSuite(IPrimeOrderGroup primeOrderGroup, IHashFunction hashFunction)
        {
            PrimeOrderGroup = primeOrderGroup;
            HashFunction = hashFunction;
        }

        public IPrimeOrderGroup PrimeOrderGroup { get; }
        public IHashFunction HashFunction { get; }
    }
}
