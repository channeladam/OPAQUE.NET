using System.Collections.Generic;
using System.Security.Cryptography;
using Opaque.Net.Abstractions;

namespace Opaque.Net.Internal
{
    public static class HashToCurveUtils
    {
        private static readonly byte[] _zeroAsByteArray = new byte[] { 0 };
        private static readonly byte[] _oneAsByteArray = new byte[] { 1 };

        /// <summary>
        /// Expands a byte string and domain separation tag into a uniformly random byte string.
        /// </summary>
        /// <param name="message">A byte string containing the message to hash.</param>
        /// <param name="domainSeparationTag">A byte string that acts as a domain separation tag.</param>
        /// <param name="outputLengthInBytes">The number of bytes to be generated.</param>
        /// <param name="hashFunction">The hash function to be used.</param>
        /// <remarks>
        /// <p>
        /// https://tools.ietf.org/html/draft-irtf-cfrg-hash-to-curve-10#section-5.4.1
        /// </p>
        /// <p>
        /// The expand_message_xmd function produces a uniformly random byte
        ///    string using a cryptographic hash function H that outputs b bits.
        ///    For security, H must meet the following requirements:
        /// </p>
        /// <p>
        ///    *  The number of bits output by H MUST be b >= 2 * k, for k the
        ///       target security level in bits, and b MUST be divisible by 8.  The
        ///       first requirement ensures k-bit collision resistance; the second
        ///       ensures uniformity of expand_message_xmd's output.
        /// </p>
        /// <p>
        ///    *  H MAY be a Merkle-Damgaard hash function like SHA-2.  In this
        ///       case, security holds when the underlying compression function is
        ///       modeled as a random oracle [CDMP05].  (See Section 10.3 for
        ///       discussion.)
        /// </p>
        /// <p>
        ///    *  H MAY be a sponge-based hash function like SHA-3 or BLAKE2.  In
        ///       this case, security holds when the inner function is modeled as a
        ///       random transformation or as a random permutation [BDPV08].
        /// </p>
        /// <p>
        ///    *  Otherwise, H MUST be a hash function that has been proved
        ///       indifferentiable from a random oracle [MRH04] under a reasonable
        ///       cryptographic assumption.
        /// </p>
        /// <p>
        ///    SHA-2 [FIPS180-4] and SHA-3 [FIPS202] are typical and RECOMMENDED
        ///    choices.  As an example, for the 128-bit security level, b >= 256
        ///    bits and either SHA-256 or SHA3-256 would be an appropriate choice.
        /// </p>
        /// <p>
        ///    The hash function H is assumed to work by repeatedly ingesting fixed-
        ///    length blocks of data.  The length of these blocks is called the
        ///    input block size.  As examples, the input block size of SHA-512
        ///    [FIPS180-4] is 128 bytes and the input block size of SHA3-512
        ///    [FIPS202] is 72 bytes.
        /// </p>
        /// <p>
        ///    The following procedure implements expand_message_xmd.
        /// </p>
        /// <p>
        ///    expand_message_xmd(msg, DST, len_in_bytes)
        /// </p>
        /// <p>
        ///    Parameters:
        ///    - H, a hash function (see requirements above).
        ///    - b_in_bytes, b / 8 for b the output size of H in bits.
        ///      For example, for b = 256, b_in_bytes = 32.
        ///    - r_in_bytes, the input block size of H, measured in bytes (see
        ///      discussion above). For example, for SHA-256, r_in_bytes = 64.
        /// </p>
        /// <p>
        ///    Input:
        ///    - msg, a byte string.
        ///    - DST, a byte string of at most 255 bytes.
        ///      See below for information on using longer DSTs.
        ///    - len_in_bytes, the length of the requested output in bytes.
        /// </p>
        /// <p>
        ///    Output:
        ///    - uniform_bytes, a byte string.
        /// </p>
        /// <p>
        ///    Steps:
        ///    1.  ell = ceil(len_in_bytes / b_in_bytes)
        ///    2.  ABORT if ell > 255
        ///    3.  DST_prime = DST || I2OSP(len(DST), 1)
        ///    4.  Z_pad = I2OSP(0, r_in_bytes)
        ///    5.  l_i_b_str = I2OSP(len_in_bytes, 2)
        ///    6.  msg_prime = Z_pad || msg || l_i_b_str || I2OSP(0, 1) || DST_prime
        ///    7.  b_0 = H(msg_prime)
        ///    8.  b_1 = H(b_0 || I2OSP(1, 1) || DST_prime)
        ///    9.  for i in (2, ..., ell):
        ///    10.    b_i = H(strxor(b_0, b_(i - 1)) || I2OSP(i, 1) || DST_prime)
        ///    11. uniform_bytes = b_1 || ... || b_ell
        ///    12. return substr(uniform_bytes, 0, len_in_bytes)
        /// </p>
        /// <p>
        ///    Note that the string Z_pad is prefixed to msg when computing b_0
        ///    (step 7).  This is necessary for security when H is a Merkle-Damgaard
        ///    hash, e.g., SHA-2 (see Section 10.3).  Hashing this additional data
        ///    means that the cost of computing b_0 is higher than the cost of
        ///    simply computing H(msg).  In most settings this overhead is
        ///    negligible, because the cost of evaluating H is much less than the
        ///    other costs involved in hashing to a curve.
        /// </p>
        /// <p>
        ///    It is possible, however, to entirely avoid this overhead by taking
        ///    advantage of the fact that Z_pad depends only on H, and not on the
        ///    arguments to expand_message_xmd.  To do so, first precompute and save
        ///    the internal state of H after ingesting Z_pad.  Then, when computing
        ///    b_0, initialize H using the saved state.  Further details are
        ///    implementation dependent, and beyond the scope of this document.
        /// </p>
        /// </remarks>
        public static byte[] ExpandMessageXmd(
            byte[] message,
            byte[] domainSeparationTag,
            int outputLengthInBytes,
            IHashFunction hashFunction)
        {
            if (domainSeparationTag.Length > 255)
            {
                throw new CryptographicException("The length of the Domain Separation Tag (DST) must be at most 255 bytes");
            }

            if (outputLengthInBytes <= 0)
            {
                throw new CryptographicException("The output length in bytes must be a positive integer");
            }

            // Known parameters:
            // - H - the hash function
            // - b_in_bytes, b / 8 for b the output size of H in bits.
            // - r_in_bytes, the input block size of H, measured in bytes

            // 1.  ell = ceil(len_in_bytes / b_in_bytes)
            int ell = 1 + ((outputLengthInBytes - 1) / hashFunction.OutputSizeInBytes);

            // 2.  ABORT if ell > 255
            if (ell > 255)
            {
                throw new CryptographicException("The length in bytes of the requested output is too large for the hash function. It must be at most 255 bytes.");
            }

            // 3.  DST_prime = DST || I2OSP(len(DST), 1)
            byte[] dstPrime = ByteArrayUtils.Concatenate(new List<byte[]>
            {
                domainSeparationTag,
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(domainSeparationTag.Length, 1)
            });

            // 4.  Z_pad = I2OSP(0, r_in_bytes)
            byte[] zPad = new byte[hashFunction.InputBlockSizeInBytes]; // already zero filled

            // 5.  l_i_b_str = I2OSP(len_in_bytes, 2)
            byte[] resultLengthInBytes = BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(outputLengthInBytes, 2);

            // 6.  msg_prime = Z_pad || msg || l_i_b_str || I2OSP(0, 1) || DST_prime
            byte[] messagePrime = ByteArrayUtils.Concatenate(new List<byte[]>
            {
                zPad,
                message,
                resultLengthInBytes,
                _zeroAsByteArray,
                dstPrime
            });

            // 7.  b_0 = H(msg_prime)
            byte[] b0 = hashFunction.HashData(messagePrime);

            // 8.  b_1 = H(b_0 || I2OSP(1, 1) || DST_prime)
            byte[] bi = hashFunction.HashData(
                ByteArrayUtils.Concatenate(new List<byte[]>
                {
                    b0,
                    _oneAsByteArray,
                    dstPrime
                }));

            // 11. uniform_bytes = b_1 || ... || b_ell
            int targetIndex = 0;
            byte[] uniformBytes = new byte[hashFunction.OutputSizeInBytes * ell];
            bi.CopyTo(uniformBytes, targetIndex);
            targetIndex += hashFunction.OutputSizeInBytes;

            // 9.  for i in (2, ..., ell):
            for (int index = 1; index < ell; index++)
            {
                // 10.    b_i = H(strxor(b_0, b_(i - 1)) || I2OSP(i, 1) || DST_prime)
                bi = hashFunction.HashData(
                    ByteArrayUtils.Concatenate(new List<byte[]>
                    {
                        ByteArrayUtils.Xor(b0, bi),
                        new byte[] { (byte)index },  // ell is always <= 255 so cast will never overflow
                        dstPrime
                    }));

                // 11. uniform_bytes = b_1 || ... || b_ell
                bi.CopyTo(uniformBytes, targetIndex);
                targetIndex += hashFunction.OutputSizeInBytes;
            }

            // 12. return substr(uniform_bytes, 0, len_in_bytes)
            return uniformBytes[0..outputLengthInBytes];
        }
    }
}
