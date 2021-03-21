using System;

namespace Opaque.Net.Abstractions
{
    public interface IProtocolContext
    {
        ReadOnlySpan<byte> ContextString { get; }
        ObliviousPseudoRandomFunctionCipherSuite CipherSuiteName { get; }
        CipherSuite CipherSuite { get; }
    }
}
