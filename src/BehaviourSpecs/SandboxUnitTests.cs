#nullable disable

using System.Runtime.InteropServices;
using ChannelAdam.TestFramework.NUnit.Abstractions;
using NUnit.Framework;
using Opaque.Net;
using Opaque.Net.Abstractions;
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
            const ObliviousPseudoRandomFunctionCipherSuite agreedCipherSuite =
                ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512;

            CryptoUtils.InitialiseCryptography();
            LogAssert.AreEqual("Sodium library version", "1.0.18", CryptoUtils.GetSodiumLibraryVersion());

            ClientContext clientContext = new(agreedCipherSuite);
            ServerContext serverContext = new(agreedCipherSuite);
        }
    }
}
