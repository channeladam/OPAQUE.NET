using System;

namespace Opaque.Net.Abstractions
{
    public interface IObliviousPseudoRandomFunctionBaseModeVariant
    {
        /// <summary>
        /// In the base mode, a client and server interact to
        /// compute y = F(skS, x), where x is the client's input, skS is the
        /// server's private key, and y is the OPRF output.
        /// The client learns y and the server learns nothing.
        /// </summary>
        Span<byte> Compute(Span<byte> serverPrivateKey, Span<byte> clientsInput);
    }
}
