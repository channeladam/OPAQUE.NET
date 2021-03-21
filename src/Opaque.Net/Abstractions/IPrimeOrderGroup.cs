using System;

namespace Opaque.Net.Abstractions
{
    public interface IPrimeOrderGroup
    {
        /// <summary>
        /// The length of a byte array for a Group Element.
        /// </summary>
        int GroupElementBytesLength { get; }

        /// <summary>
        /// The length of a byte array for a Scalar value.
        /// </summary>
        int ScalarBytesLength { get; }

        /// <summary>
        /// The length of a byte array for a Hash.
        /// </summary>
        int HashBytesLength { get; }

        /// <summary>
        /// Generates a random Group Element.
        /// </summary>
        ReadOnlySpan<byte> GenerateRandomGroupElement();

        /// <summary>
        /// Generates a random Scalar value.
        /// </summary>
        ReadOnlySpan<byte> GenerateRandomScalar();

        /// <summary>
        /// Perform scalar multiplication of a group element with a scalar value.
        /// </summary>
        /// <param name="nScalar">The scalar that 'p' is multiplied by.</param>
        /// <param name="pGroupElement">The group element that is multiplied by 'n'.</param>
        /// <returns>A group element 'q'. NOTE: this should NOT be used as a shared key prior to hashing.</returns>
        ReadOnlySpan<byte> ScalarMult(ReadOnlySpan<byte> nScalar, ReadOnlySpan<byte> pGroupElement);
    }
}
