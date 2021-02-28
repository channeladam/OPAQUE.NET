using System;
using System.Security.Cryptography;

namespace Opaque.Net.Abstractions
{
    /// <summary>
    /// Represents a public/private key pair for an elliptic curve cryptography (ECC) algorithm.
    /// </summary>
    public class KeyPair
    {
        private ECParameters _ecParameters;

        public KeyPair(ECParameters ecParameters)
        {
            _ecParameters = ecParameters;
        }

        /// <summary>
        /// Represents the private key D for the elliptic curve cryptography (ECC) algorithm,
        /// stored in big-endian format.
        /// </summary>
        public ReadOnlySpan<byte> PrivateKey => _ecParameters.D;

        /// <summary>
        /// Represents the public key Q for the elliptic curve cryptography (ECC) algorithm.
        /// This is an (X,Y) coordinate pair for elliptic curve cryptography (ECC) structures.
        /// </summary>
        public ECPoint PublicKeyPoint => _ecParameters.Q;

        /// <summary>
        /// The X coordinate of the Public Key.
        /// </summary>
        public ReadOnlySpan<byte> PublicKeyX => _ecParameters.Q.X;

        /// <summary>
        /// The Y coordinate of the Public Key.
        /// </summary>
        public ReadOnlySpan<byte> PublicKeyY => _ecParameters.Q.Y;
    }
}
