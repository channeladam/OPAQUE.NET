using System.Security;

namespace Opaque.Net.Abstractions
{
    public interface IClientContext
    {
        /// <summary>
        /// Blinding mechanism for the user input - randomising it with a Scalar and converting it into a Group Element.
        /// </summary>
        ClientContextBlindResult Blind(SecureString clientInput);

        /// <summary>
        /// Blinding mechanism for the user input - randomising it with a Scalar and converting it into a Group Element.
        /// </summary>
        ClientContextBlindResult Blind(byte[] clientInput);

        /// <summary>
        /// Unblinds the server's evaluated result, verifies the server's proof if verifiability is required,
        /// and produces a byte array corresponding to the output of the OPRF protocol from the server's evaluated result.
        /// </summary>
        byte[] Finalise(byte[] clientInput, byte[] blindScalar, ServerContextEvaluatedResult serverEvaluatedResult);
    }
}
