namespace Opaque.Net
{
    /// <summary>
    /// Cipher suites for Oblivious Pseudorandom Functions (OPRFs) using Prime-Order Groups.
    /// A cipher suite consists of both a prime-order group and a hash.
    /// </summary>
    /// <remarks>
    /// See https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.
    /// Security note: https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-6.2.4:
    ///  "For applications that cannot tolerate discrete logarithm security of
    ///   lower than 128 bits, we recommend only implementing ciphersuites with
    ///   IDs: 0x0002, 0x0004, and 0x0005."
    /// Security Note: P384 is not safe according to https://safecurves.cr.yp.to/
    /// </remarks>
    public enum ObliviousPseudoRandomFunctionCipherSuite
    {
        None = 0,

        // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.1
        Ristretto255_SHA512 = 0x0001,

        // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.2
        Decaf448_SHA512 = 0x0002,

        // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.3
        P256_SHA256 = 0x0003,

        // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.4
        P384_SHA256 = 0x0004,

        // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-5.5
        P521_SHA256 = 0x0005
    }
}
