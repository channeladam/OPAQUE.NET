#nullable disable

using System.Linq;
using ChannelAdam.TestFramework.NUnit.Abstractions;
using Opaque.Net;
using Opaque.Net.Abstractions;
using TechTalk.SpecFlow;

namespace BehaviourSpecs
{
    [Binding]
    [Scope(Feature = "Protocol")]
    public class ProtocolUnitTests : TestEasy
    {
        private KeyPair _keyPair1;
        private int _expectedKeyLength;
        private ProtocolContext _protocolContext;
        private byte[] _actualContextString;
        private byte[] _expectedContextString;

        // #region Before/After

        // [BeforeScenario]
        // public void BeforeScenario()
        // {
        // }

        // #endregion

        // #region Given

        // [Given("")]
        // public void Given()
        // {
        // }

        // #endregion

        #region When

        [When("a public-private key pair is generated")]
        public void WhenAPublicPrivateKeyPairIsGenerated()
        {
            _keyPair1 = CryptoUtils.GenerateKeyPair(ObliviousPseudoRandomFunctionCipherSuite.P384_SHA256);
            _expectedKeyLength = 384 / 8;
        }

        [When("the Context String for a protocol context is generated")]
        public void WhenTheContextStringForAProtocolContextIsGenerated()
        {
            _protocolContext = new ServerContext(ObliviousPseudoRandomFunctionCipherSuite.P384_SHA256);
            _actualContextString = _protocolContext.ContextString.ToArray();
            _expectedContextString = new byte[] { 0, 0, 4 }; // Mode 0, Cipher 0x0004
        }

        // [When("")]
        // public void When()
        // {
        // }

        #endregion

        #region Then

        [Then("the public and private keys are available")]
        public void ThenThePublicAndPrivateKeysAreAvailable()
        {
            LogAssert.AreEqual("Private key is populated with expected length", _expectedKeyLength, _keyPair1.PrivateKey.Length);
            LogAssert.IsTrue("Private key is not all byte 255", _keyPair1.PrivateKey.ToArray().All(b => b != 255));

            LogAssert.AreEqual("Public key X coordinate is populated with expected length", _expectedKeyLength, _keyPair1.PublicKeyX.Length);
            LogAssert.IsTrue("Public key X coordinate is not all byte 255", _keyPair1.PublicKeyX.ToArray().All(b => b != 255));

            LogAssert.AreEqual("Public key Y coordinate is populated with expected length", _expectedKeyLength, _keyPair1.PublicKeyY.Length);
            LogAssert.IsTrue("Public key Y coordinate is not all byte 255", _keyPair1.PublicKeyY.ToArray().All(b => b != 255));
        }

        [Then("the Context String is as expected")]
        public void ThenTheContextStringIsAsExpected()
            => LogAssert.IsTrue("Context String is as expected", _expectedContextString.SequenceEqual(_actualContextString));

        // [Then("")]
        // public void Then()
        // {
        // }

        #endregion
    }
}
