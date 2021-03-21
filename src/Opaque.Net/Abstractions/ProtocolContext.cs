using System;
using Opaque.Net.Internal;

namespace Opaque.Net.Abstractions
{
    public abstract class ProtocolContext : IProtocolContext
    {
        protected ProtocolContext(ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName)
        {
            CipherSuiteName = cipherSuiteName;
            CipherSuite = CipherSuiteProviderFactory.Create(cipherSuiteName);
        }

        public static ObliviousPseudoRandomFunctionProtocolMode ProtocolMode => ObliviousPseudoRandomFunctionProtocolMode.Base;

        public ReadOnlySpan<byte> ContextString
        {
            get
            {
                byte[] result = new byte[3];
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive((uint)ProtocolMode, 1, ref result, 0);
                BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive((uint)CipherSuiteName, 2, ref result, 1);
                return result;
            }
        }

        public ObliviousPseudoRandomFunctionCipherSuite CipherSuiteName { get; }

        public CipherSuite CipherSuite { get; }
    }
}
