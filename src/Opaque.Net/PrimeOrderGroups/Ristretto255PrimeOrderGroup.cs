using System;
using System.Security.Cryptography;
using Opaque.Net.Abstractions;

namespace Opaque.Net.PrimeOrderGroups
{
    public class Ristretto255PrimeOrderGroup : IPrimeOrderGroup
    {
        private readonly Lazy<int> _elementBytesLength = new(() =>
            (int)Sodium.crypto_core_ristretto255.CryptoCoreRistretto255Bytes());

        private readonly Lazy<int> _scalarBytesLength = new(() =>
            (int)Sodium.crypto_core_ristretto255.CryptoCoreRistretto255Scalarbytes());

        private readonly Lazy<int> _hashBytesLength = new(() =>
            (int)Sodium.crypto_core_ristretto255.CryptoCoreRistretto255Hashbytes());

        /// <inheritdoc />
        public int GroupElementBytesLength => _elementBytesLength.Value;

        /// <inheritdoc />
        public int ScalarBytesLength => _scalarBytesLength.Value;

        /// <inheritdoc />
        public int HashBytesLength => _hashBytesLength.Value;

        /// <inheritdoc />
        public unsafe ReadOnlySpan<byte> GenerateRandomGroupElement()
        {
            byte[] result = new byte[GroupElementBytesLength];

            fixed (byte* resultPointer = &result[0])
            {
                Sodium.crypto_core_ristretto255.CryptoCoreRistretto255Random(resultPointer);
            }

            return result;
        }

        /// <inheritdoc />
        public unsafe ReadOnlySpan<byte> GenerateRandomScalar()
        {
            byte[] result = new byte[ScalarBytesLength];

            fixed (byte* resultPointer = &result[0])
            {
                Sodium.crypto_core_ristretto255.CryptoCoreRistretto255ScalarRandom(resultPointer);
            }

            return result;
        }

        /// <inheritdoc />
        public unsafe ReadOnlySpan<byte> ScalarMult(ReadOnlySpan<byte> nScalar, ReadOnlySpan<byte> pGroupElement)
        {
            nScalar.ValidateLength(ScalarBytesLength, "n");
            pGroupElement.ValidateLength(GroupElementBytesLength, "p");

            byte[] qGroupElementResult = new byte[GroupElementBytesLength];

            fixed (byte* qResultPointer = &qGroupElementResult[0],
                         nScalarPointer = &nScalar.GetPinnableReference(),
                         pGroupElementPointer = &pGroupElement.GetPinnableReference())
            {
                // 0 on success, -1 on error
                int success = Sodium.crypto_scalarmult_ristretto255.CryptoScalarmultRistretto255(qResultPointer, nScalarPointer, pGroupElementPointer);
                if (success != 0)
                {
                    // -1 if q is the identity element
                    throw new CryptographicException("Sodium Library: crypto_scalarmult_ristretto255() has failed");
                }
            }

            return qGroupElementResult;
        }
    }
}
