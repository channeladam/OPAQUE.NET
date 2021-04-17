using System;
using System.Collections.Generic;
using Opaque.Net.Abstractions;
using Opaque.Net.Internal;

namespace Opaque.Net
{
    public class CipherSuite : ICipherSuite
    {
        /// <remarks>
        /// DST Prefix - https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.1
        /// </remarks>
        private const string DomainSeparationTagPrefix = "VOPRF06";

        private static readonly byte[] _hyphenBytes = "-".ConvertStringAsUtf8ToByteArray();
        private static readonly byte[] _domainSeparationTagPrefixBytes = DomainSeparationTagPrefix.ConvertStringAsUtf8ToByteArray();

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

        public byte[] CreateDomainSeparationTag(string functionName)
            => ByteArrayUtils.Concatenate(new List<byte[]>
            {
                _domainSeparationTagPrefixBytes,
                _hyphenBytes,
                functionName.ConvertStringAsUtf8ToByteArray(),
                _hyphenBytes,
                ProtocolContextString
            });

        #region Private Methods

        private void EnsureCryptoIsInitialised()
        {
            bool _ = _lazyCryptoInitialisation.Value;
        }

        #endregion Private Methods
    }
}
