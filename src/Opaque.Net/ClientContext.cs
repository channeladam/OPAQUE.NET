using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using Opaque.Net.Abstractions;
using Opaque.Net.Internal;

namespace Opaque.Net
{
    public class ClientContext : ProtocolContext, IClientContext
    {
        public ClientContext(CipherSuite cipherSuite) : base(cipherSuite)
        {
        }

        /// <inheritdoc />
        public ClientContextBlindResult Blind(SecureString clientInput)
            => Blind(clientInput.ConvertToByteArray());

        /// <inheritdoc />
        /// <remarks>
        /// <p>
        /// Specification: https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3.4.3.1.
        ///   Input:
        ///     ClientInput input
        ///   Output:
        ///     Scalar blind
        ///     SerializedElement blindedElement
        ///   def Blind(input):
        ///     blind = GG.RandomScalar()
        ///     P = GG.HashToGroup(input)
        ///     blindedElement = GG.SerializeElement(blind * P)
        ///     return blind, blindedElement
        /// </p>
        /// <p>
        /// This algorithm uses 'multiplicative binding'.
        /// 'Additive blinding' with offline pre-processing is an alternative more performant approach:
        ///   - see https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-7
        /// </p>
        /// </remarks>
        public ClientContextBlindResult Blind(byte[] clientInput)
        {
            // blind = GG.RandomScalar()
            byte[] blindScalar = CipherSuite.PrimeOrderGroup.GenerateRandomScalar();

            // P = GG.HashToGroup(input)
            byte[] pGroupElement = CipherSuite.PrimeOrderGroup.HashToGroup(clientInput);

            // blindedElement = GG.SerializeElement(blind * P)
            byte[] blindedGroupElement = CipherSuite.PrimeOrderGroup.PerformScalarMultiplication(blindScalar, pGroupElement);

            // return blind, blindedElement
            return new ClientContextBlindResult(blindScalar, blindedGroupElement);
        }

        /// <inheritdoc />
        /// <remarks>
        /// Specification: https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3.4.3.3
        /// .
        /// Input:
        ///     ClientInput input
        ///     Scalar blind
        ///     SerializedElement evaluatedElement
        /// Output:
        ///     opaque output[Nh]
        /// def Finalize(input, blind, evaluatedElement):
        ///     unblindedElement = Unblind(blind, evaluatedElement)
        ///     finalizeDST = "VOPRF06-Finalize-" || self.contextString
        ///     hashInput = I2OSP(len(input), 2) || input ||
        ///                 I2OSP(len(unblindedElement), 2) || unblindedElement ||
        ///                 I2OSP(len(finalizeDST), 2) || finalizeDST
        ///     return Hash(hashInput)
        /// </remarks>
        public byte[] Finalise(byte[] clientInput, byte[] blindScalar, ServerContextEvaluatedResult serverEvaluatedResult)
        {
            // unblindedElement = Unblind(blind, evaluatedElement)
            byte[] unblindedGroupElement = Unblind(blindScalar, serverEvaluatedResult.EvaluatedGroupElement);

            // DST, a domain separation tag
            // as per https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.1
            // finalizeDST = "VOPRF06-Finalize-" || self.contextString
            byte[] finaliseDST = CipherSuite.CreateDomainSeparationTag("Finalize");

            // hashInput = I2OSP(len(input), 2) || input ||
            //             I2OSP(len(unblindedElement), 2) || unblindedElement ||
            //             I2OSP(len(finalizeDST), 2) || finalizeDST
            byte[] hashInput = ByteArrayUtils.Concatenate(new List<byte[]>
            {
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(clientInput.Length, 2),
                clientInput,
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(unblindedGroupElement.Length, 2),
                unblindedGroupElement,
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(finaliseDST.Length, 2),
                finaliseDST
            });

            // return Hash(hashInput)
            return CipherSuite.HashFunction.HashData(hashInput);
        }

        #region Private Methods

        /// <summary>
        /// Unblinds the server's evaluated result.
        /// </summary>
        /// <returns>Unblinded Group Element.</returns>
        /// <remarks>
        /// Specification: https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3.4.3.2
        /// Input:
        ///     Scalar blind
        ///     SerializedElement evaluatedElement
        /// Output:
        ///     SerializedElement unblindedElement
        /// def Unblind(blind, evaluatedElement, ...):
        ///     Z = GG.DeserializeElement(evaluatedElement)
        ///     N = (blind^(-1)) * Z
        ///     unblindedElement = GG.SerializeElement(N)
        ///     return unblindedElement
        /// </remarks>
        private byte[] Unblind(byte[] blindScalar, byte[] serverEvaluatedGroupElement)
        {
            if (!CipherSuite.PrimeOrderGroup.IsValidPoint(serverEvaluatedGroupElement))
            {
                throw new CryptographicException("Provided Server Context Evaluated Group Element is not a valid point on the curve");
            }

            // blind^(-1)
            // i.e. Invert the random blind scalar that was used to blind the client input
            byte[] invertedBlindScalar = CipherSuite.PrimeOrderGroup.InvertScalar(blindScalar);

            // Z = GG.DeserializeElement(evaluatedElement)
            // N = (blind^(-1)) * Z
            // unblindedElement = GG.SerializeElement(N)
            return CipherSuite.PrimeOrderGroup.PerformScalarMultiplication(invertedBlindScalar, serverEvaluatedGroupElement);
        }

        #endregion Private Methods
    }
}
