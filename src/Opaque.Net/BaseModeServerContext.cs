using System;
using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    /// <summary>
    /// An implementation of the Server Context when operating in the OPRF Base Mode.
    /// </summary>
    public class BaseModeServerContext : ProtocolContext, IBaseModeServerContext
    {
        public BaseModeServerContext(CipherSuite cipherSuite) : base(cipherSuite)
        {
            if (CipherSuite.ProtocolMode == ObliviousPseudoRandomFunctionProtocolMode.Base)
            {
                return;
            }

            throw new InvalidOperationException("The Cipher Suite Protocol Mode must be 'Base'");
        }

        /// <summary>
        /// Evaluates the client's blinded result.
        /// </summary>
        /// <param name="skSServerPrivateKeyScalar">The secret key of the server (skS) - i.e. the server's private key as a scalar value.</param>
        /// <param name="clientBlindedGroupElement">The serialised blinded element from the client - i.e. the blinded user's password.</param>
        /// <returns>A serialised group element - to be an input to an Unblind function.</returns>
        /// <remarks>
        /// Base Mode Specification: see https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3.4.1.1.
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
        public ServerContextEvaluatedResult Evaluate(byte[] skSServerPrivateKeyScalar, byte[] clientBlindedGroupElement)
        {
            byte[] evaluatedGroupElement = CipherSuite.PrimeOrderGroup.PerformScalarMultiplication(skSServerPrivateKeyScalar, clientBlindedGroupElement);
            return new ServerContextEvaluatedResult(evaluatedGroupElement);
        }
    }
}
