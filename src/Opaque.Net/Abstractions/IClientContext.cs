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
    }
}
