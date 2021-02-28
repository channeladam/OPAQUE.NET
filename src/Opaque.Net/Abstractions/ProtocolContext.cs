using System;
using Opaque.Net.Abstractions;

namespace Opaque.Net
{
    public abstract class ProtocolContext : IProtocolContext
    {
        protected ProtocolContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuite)
        {
            CipherSuite = cipherSuite;
        }

        public ReadOnlySpan<byte> ContextString
        {
            get
            {
                byte[] result = new byte[3];
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive((uint)ProtocolMode, 1, ref result, 0);
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive((uint)CipherSuite, 2, ref result, 1);
                return result;
            }
        }

        public ObliviousPseudoRandomFunctionCipherSuite CipherSuite { get; }

        public static ObliviousPseudoRandomFunctionProtocolMode ProtocolMode
            => ObliviousPseudoRandomFunctionProtocolMode.Base;
    }
}
