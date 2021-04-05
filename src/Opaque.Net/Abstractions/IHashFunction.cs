namespace Opaque.Net.Abstractions
{
    public interface IHashFunction
    {
        ushort InputBlockSizeInBytes { get; }
        ushort OutputSizeInBits { get; }
        ushort OutputSizeInBytes { get; }

        byte[] HashData(byte[] source);
    }
}
