namespace Opaque.Net.Abstractions
{
    public interface IBaseModeClientContext : IClientContext
    {
        /// <summary>
        /// Unblinds the server's evaluated result, verifies the server's proof if verifiability is required,
        /// and produces a byte array corresponding to the output of the OPRF protocol from the server's evaluated result.
        /// </summary>
        byte[] Finalise(byte[] clientInput, byte[] blindScalar, ServerContextEvaluatedResult serverEvaluatedResult);
    }
}
