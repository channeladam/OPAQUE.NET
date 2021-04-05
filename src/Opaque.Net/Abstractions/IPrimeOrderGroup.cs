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
        byte[] GenerateRandomGroupElement();

        /// <summary>
        /// Generates a random Scalar value.
        /// </summary>
        byte[] GenerateRandomScalar();

        /// <summary>
        /// Hash the given message to a Group Element.
        /// </summary>
        /// <returns>A Group Element 'P'.</returns>
        /// <remarks>
        /// hash_to_curve(msg) - https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-hash-to-curve-10
        ///  Input: msg, an arbitrary-length byte string.
        ///  Output: P, a point in G.
        /// .
        ///  Steps:
        ///    1. u = hash_to_field(msg, 2)
        ///    2. Q0 = map_to_curve(u[0])
        ///    3. Q1 = map_to_curve(u[1])
        ///    4. R = Q0 + Q1              # Point addition
        ///    5. P = clear_cofactor(R)
        ///    6. return P
        /// .
        ///  Each hash-to-curve suite in Section 8 instantiates one of these encoding functions for a specific elliptic curve.
        /// </remarks>
        byte[] HashToGroup(byte[] messageToHash);

        /// <summary>
        /// Perform scalar multiplication of a group element with a scalar value.
        /// </summary>
        /// <param name="nScalar">The scalar that 'p' is multiplied by.</param>
        /// <param name="pGroupElement">The group element that is multiplied by 'n'.</param>
        /// <returns>A group element 'q'. NOTE: this should NOT be used as a shared key prior to hashing.</returns>
        byte[] ScalarMult(byte[] nScalar, byte[] pGroupElement);
    }
}
