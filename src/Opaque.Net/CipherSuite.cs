using System;
using Opaque.Net.Abstractions;
using Opaque.Net.Internal;

namespace Opaque.Net
{
    public class CipherSuite : ICipherSuite
    {
        private readonly Lazy<bool> _lazyCryptoInitialisation = new(() =>
        {
            CryptoUtils.InitialiseCryptography();
            return true;
        });

        private readonly IPrimeOrderGroupFactory _primeOrderGroupFactory;
        private readonly Lazy<IPrimeOrderGroup> _lazyPrimeOrderGroup;
        private readonly IHashFunctionFactory _hashFunctionFactory;
        private readonly Lazy<IHashFunction> _lazyHashFunction;

        public CipherSuite(
            ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName,
            IPrimeOrderGroupFactory primeOrderGroupFactory,
            IHashFunctionFactory hashFunctionFactory)
                : this(cipherSuiteName, ObliviousPseudoRandomFunctionProtocolMode.Base, primeOrderGroupFactory, hashFunctionFactory)
        {
        }

        public CipherSuite(
            ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName,
            ObliviousPseudoRandomFunctionProtocolMode protocolMode,
            IPrimeOrderGroupFactory primeOrderGroupFactory,
            IHashFunctionFactory hashFunctionFactory)
        {
            CipherSuiteName = cipherSuiteName;
            ProtocolMode = protocolMode;

            _primeOrderGroupFactory = primeOrderGroupFactory ?? throw new ArgumentNullException(nameof(primeOrderGroupFactory));
            _lazyPrimeOrderGroup = new(() => _primeOrderGroupFactory.Create(cipherSuiteName, this));

            _hashFunctionFactory = hashFunctionFactory ?? throw new ArgumentNullException(nameof(hashFunctionFactory));
            _lazyHashFunction = new(() => _hashFunctionFactory.Create(cipherSuiteName, this));

            EnsureCryptoIsInitialised();
        }

        public ObliviousPseudoRandomFunctionCipherSuite CipherSuiteName { get; }
        public ObliviousPseudoRandomFunctionProtocolMode ProtocolMode { get; }
        public IPrimeOrderGroup PrimeOrderGroup => _lazyPrimeOrderGroup.Value;
        public IHashFunction HashFunction => _lazyHashFunction.Value;

        public byte[] ProtocolContextString
        {
            get
            {
                byte[] result = new byte[3];
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive((int)ProtocolMode, 1, ref result, 0);
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive((int)CipherSuiteName, 2, ref result, 1);
                return result;
            }
        }

        #region Private Methods

        private void EnsureCryptoIsInitialised()
        {
            bool _ = _lazyCryptoInitialisation.Value;
        }

        #endregion Private Methods
    }
}
