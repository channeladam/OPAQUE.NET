namespace Opaque.Net.Abstractions
{
    public interface IBaseModeServerContext : IServerContext
    {
        ServerContextEvaluatedResult Evaluate(byte[] skSServerPrivateKeyScalar, byte[] clientBlindedGroupElement);
    }
}
