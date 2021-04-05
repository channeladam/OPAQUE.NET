#nullable disable

using ChannelAdam.TestFramework.NUnit.Abstractions;
using Opaque.Net;
using Opaque.Net.Internal;
using TechTalk.SpecFlow;

namespace BehaviourSpecs
{
    [Binding]
    [Scope(Feature = "Sandbox")]
    public class SandboxUnitTests : TestEasy
    {
        [When("do stuff")]
        public void DoStuff()
        {
            const ObliviousPseudoRandomFunctionCipherSuite cipherSuiteName = ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512;
            PrimeOrderGroupFactory pogFactory = new();
            HashFunctionFactory hfFactory = new();
            CipherSuite cipherSuite = new CipherSuite(cipherSuiteName, pogFactory, hfFactory);

            CryptoUtils.InitialiseCryptography();
            LogAssert.AreEqual("Sodium library version", "1.0.18", CryptoUtils.GetSodiumLibraryVersion());

            ClientContext clientContext = new(cipherSuite);
            ServerContext serverContext = new(cipherSuite);
        }
    }
}
