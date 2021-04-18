//-----------------------------------------------------------------------
// <copyright file="KeyPair.cs"
//     Copyright (c) 2021 Adam Craven. All rights reserved.
// </copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using System;
using System.Security.Cryptography;

namespace Opaque.Net
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
