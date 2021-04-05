using System;
using System.Security;
using Opaque.Net.Abstractions;

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
        public ClientContextBlindResult Blind(byte[] clientInput)
        {
            // blind = GG.RandomScalar()
            byte[] blindScalar = CipherSuite.PrimeOrderGroup.GenerateRandomScalar();

            // P = GG.HashToGroup(input)
            byte[] pGroupElement = CipherSuite.PrimeOrderGroup.HashToGroup(clientInput);

            // blindedElement = GG.SerializeElement(blind * P)
            byte[] blindedGroupElement = CipherSuite.PrimeOrderGroup.ScalarMult(blindScalar, pGroupElement);

            // return blind, blindedElement
            return new ClientContextBlindResult(blindScalar, blindedGroupElement);
        }
    }
}
