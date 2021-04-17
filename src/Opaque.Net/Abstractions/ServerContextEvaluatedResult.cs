namespace Opaque.Net.Abstractions
{
    public class ServerContextEvaluatedResult
    {
        /// <summary>
        /// Used in the base mode with verification of the server key.
        /// </summary>
        /// <param name="evaluatedGroupElement"></param>
        public ServerContextEvaluatedResult(byte[] evaluatedGroupElement) : this(evaluatedGroupElement, null)
        {
        }

        /// <summary>
        /// Used in the Verifiable OPRF mode.
        /// </summary>
        /// <param name="evaluatedGroupElement"></param>
        /// <param name="proof"></param>
        public ServerContextEvaluatedResult(byte[] evaluatedGroupElement, byte[]? proof)
        {
            EvaluatedGroupElement = evaluatedGroupElement ?? throw new System.ArgumentNullException(nameof(evaluatedGroupElement));
            Proof = proof;
        }

        public byte[] EvaluatedGroupElement { get; }
        public byte[]? Proof { get; }
    }
}
