namespace Opaque.Net
{
    /// <summary>
    /// Oblivious Pseudo-Random Function protocol variant modes of operation.
    /// </summary>
    /// <remarks>
    /// https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#section-3
    /// </remarks>
    public enum ObliviousPseudoRandomFunctionProtocolMode
    {
        Base = 0x00,
        // Verifiable = 0x01 // Not supported or needed for Opaque
    }
}
