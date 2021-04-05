using System.Security;

namespace Opaque.Net.Abstractions
{
    public interface IClientContext
    {
        /// <summary>
        /// Blinding mechanism for the user input - randomising it with a Scalar and converting it into a Group Element.
        /// </summary>
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
        /// <p>
        /// Regarding SecureString and interop:
        ///  - see https://docs.microsoft.com/en-us/dotnet/api/system.security.securestring?view=net-5.0#securestring-and-interop
        /// </p>
        /// </remarks>
        ClientContextBlindResult Blind(SecureString clientInput);

        /// <summary>
        /// Blinding mechanism for the user input - randomising it with a Scalar and converting it into a Group Element.
        /// </summary>
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
        ClientContextBlindResult Blind(byte[] clientInput);
    }
}
