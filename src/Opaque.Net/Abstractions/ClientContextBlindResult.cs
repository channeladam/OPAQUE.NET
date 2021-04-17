namespace Opaque.Net.Abstractions
{
    public class ClientContextBlindResult
    {
        public ClientContextBlindResult(byte[] blindScalar, byte[] blindedGroupElement)
        {
            BlindScalar = blindScalar;
            BlindedGroupElement = blindedGroupElement;
        }

        public byte[] BlindScalar { get; }
        public byte[] BlindedGroupElement { get; }
    }
}
