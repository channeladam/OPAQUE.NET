using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public class ServerContext : ProtocolContext
    {
        public ServerContext(CipherSuite cipherSuite) : base(cipherSuite)
        {
        }

        /// <summary>
        /// Evaluate
        /// </summary>
        /// <param name="skSServerPrivateKeyScalar">The secret key of the server (skS) - i.e. the server's private key as a scalar value.</param>
        /// <param name="clientBlindedGroupElement">The serialised blinded element from the client - i.e. the blinded user's password.</param>
        /// <returns>A serialised group element - to be an input to an Unblind function.</returns>
        /// <remarks>
        /// Specification: see https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3.4.1.1.
        ///   Input:
        ///     PrivateKey skS
        ///     SerializedElement blindedElement
        ///   Output:
        ///     SerializedElement evaluatedElement
        ///   def Evaluate(skS, blindedElement):
        ///     R = GG.DeserializeElement(blindedElement)
        ///     Z = skS * R
        ///     evaluatedElement = GG.SerializeElement(Z)
        ///     return evaluatedElement
        /// </remarks>
        public byte[] Evaluate(byte[] skSServerPrivateKeyScalar, byte[] clientBlindedGroupElement)
            => CipherSuite.PrimeOrderGroup.ScalarMult(skSServerPrivateKeyScalar, clientBlindedGroupElement);

        // NOTE: "not used in the main OPRF Protocol"
        // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3.4.1
        // public void FullEvaluate()
        // {
        // }
        // public void VerifyFinalize()
        // {
        // } 
    }
}
