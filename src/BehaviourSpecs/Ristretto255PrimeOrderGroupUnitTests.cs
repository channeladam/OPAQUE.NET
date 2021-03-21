#nullable disable

using System;
using System.Linq;
using ChannelAdam.TestFramework.NUnit.Abstractions;
using Opaque.Net;
using Opaque.Net.Abstractions;
using Opaque.Net.Internal;
using TechTalk.SpecFlow;

namespace BehaviourSpecs
{
    [Binding]
    [Scope(Feature = "Ristretto 255 Prime Order Group")]
    public class Ristretto255PrimeOrderGroupUnitTests : TestEasy
    {
        private IPrimeOrderGroup _pog;
        private byte[] _actualBytes;
        private int _expectedLength;
        private ServerContext _serverContext;
        private byte[] _testVectorSeed;
        private byte[] _testVectorSecretKeyServer;
        private byte[] _testVectorInput;
        private byte[] _testVectorBlind;
        private byte[] _testVectorBlindedElement;
        private byte[] _testVectorEvaluationElement;
        private byte[] _testVectorOutput;

        #region Before/After

        [BeforeScenario]
        public void BeforeScenario()
        {
            CryptoUtils.InitialiseCryptography();
            _pog = new Ristretto255PrimeOrderGroup();
            _serverContext = new(ObliviousPseudoRandomFunctionCipherSuite.Ristretto255_SHA512);
        }

        #endregion

        #region Given

        [Given("the values from Test Vector 1 in the specification")]
        public void GivenTheValuesFromTestVector1InTheSpecification()
        {
            // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#appendix-A.1
            //
            // A.1.1.  Base Mode
            // seed = aca1ae53bec831a1279b75ec6091b23d28034b59f77abeb0fa8f6d1a01340234
            // skSm = 758cbac0e1eb4265d80f6e6489d9a74d788f7ddeda67d7fb3c08b08f44bda30a
            //
            // A.1.1.1.  Test Vector 1, Batch Size 1
            // Input = 00
            // Blind = c604c785ada70d77a5256ae21767de8c3304115237d262134f5e46e512cf8e03
            // BlindedElement = 3c7f2d901c0d4f245503a186086fbdf5d8b4408432b25c5163e8b5a19c258348
            // EvaluationElement = fc6c2b854553bf1ed6674072ed0bde1a9911e02b4bd64aa02cfb428f30251e77
            // Output = d8ed12382086c74564ae19b7a2b5ed9bdc52656d1fc151faaae51aaba86291e8df0b2143a92f24d44d5efd0892e2e26721d27d88745343493634a66d3a925e3a

            _testVectorSeed = "aca1ae53bec831a1279b75ec6091b23d28034b59f77abeb0fa8f6d1a01340234".ConvertHexStringToByteArray();
            _testVectorSecretKeyServer = "758cbac0e1eb4265d80f6e6489d9a74d788f7ddeda67d7fb3c08b08f44bda30a".ConvertHexStringToByteArray();
            _testVectorInput = "00".ConvertHexStringToByteArray();
            _testVectorBlind = "c604c785ada70d77a5256ae21767de8c3304115237d262134f5e46e512cf8e03".ConvertHexStringToByteArray();
            _testVectorBlindedElement = "3c7f2d901c0d4f245503a186086fbdf5d8b4408432b25c5163e8b5a19c258348".ConvertHexStringToByteArray();
            _testVectorEvaluationElement = "fc6c2b854553bf1ed6674072ed0bde1a9911e02b4bd64aa02cfb428f30251e77".ConvertHexStringToByteArray();
            _testVectorOutput = "d8ed12382086c74564ae19b7a2b5ed9bdc52656d1fc151faaae51aaba86291e8df0b2143a92f24d44d5efd0892e2e26721d27d88745343493634a66d3a925e3a".ConvertHexStringToByteArray();
        }

        [Given("the values from Test Vector 2 in the specification")]
        public void GivenTheValuesFromTestVector2InTheSpecification()
        {
            // https://datatracker.ietf.org/doc/html/draft-irtf-cfrg-voprf-06.txt#appendix-A.1
            //
            // A.1.1.  Base Mode
            // seed = aca1ae53bec831a1279b75ec6091b23d28034b59f77abeb0fa8f6d1a01340234
            // skSm = 758cbac0e1eb4265d80f6e6489d9a74d788f7ddeda67d7fb3c08b08f44bda30a
            //
            // A.1.1.2.  Test Vector 2, Batch Size 1
            // Input = 5a5a5a5a5a5a5a5a5a5a5a5a5a5a5a5a5a
            // Blind = 5ed895206bfc53316d307b23e46ecc6623afb3086da74189a416012be037e50b
            // BlindedElement = 28a5e797b710f76d20a52507145fbf320a574ec2c8ab0e33e65dd2c277d0ee56
            // EvaluationElement = 345e140b707257ae83d4911f7ead3177891e7a62c54097732802c4c7a98ab25a
            // Output = 4d5f4221b5ebfd4d1a9dd54830e1ed0bce5a8f30a792723a6fddfe6cfe9f86bb1d95a3725818aeb725eb0b1b52e01ee9a72f47042372ef66c307770054d674fc

            _testVectorSeed = "aca1ae53bec831a1279b75ec6091b23d28034b59f77abeb0fa8f6d1a01340234".ConvertHexStringToByteArray();
            _testVectorSecretKeyServer = "758cbac0e1eb4265d80f6e6489d9a74d788f7ddeda67d7fb3c08b08f44bda30a".ConvertHexStringToByteArray();
            _testVectorInput = "5a5a5a5a5a5a5a5a5a5a5a5a5a5a5a5a5a".ConvertHexStringToByteArray();
            _testVectorBlind = "5ed895206bfc53316d307b23e46ecc6623afb3086da74189a416012be037e50b".ConvertHexStringToByteArray();
            _testVectorBlindedElement = "28a5e797b710f76d20a52507145fbf320a574ec2c8ab0e33e65dd2c277d0ee56".ConvertHexStringToByteArray();
            _testVectorEvaluationElement = "345e140b707257ae83d4911f7ead3177891e7a62c54097732802c4c7a98ab25a".ConvertHexStringToByteArray();
            _testVectorOutput = "4d5f4221b5ebfd4d1a9dd54830e1ed0bce5a8f30a792723a6fddfe6cfe9f86bb1d95a3725818aeb725eb0b1b52e01ee9a72f47042372ef66c307770054d674fc".ConvertHexStringToByteArray();
        }

        #endregion

        #region When

        [When("a random group element is generated")]
        public void WhenARandomGroupElementIsGenerated()
        {
            _actualBytes = _pog.GenerateRandomGroupElement().ToArray();
            _expectedLength = _pog.GroupElementBytesLength;
        }

        [When("a random scalar value is generated")]
        public void WhenARandomScalarValueIsGenerated()
        {
            _actualBytes = _pog.GenerateRandomScalar().ToArray();
            _expectedLength = _pog.ScalarBytesLength;
        }

        [When("the Server Context performs evaluation")]
        public void WhenTheServerContextPerformsEvaluation()
            => _actualBytes = _serverContext.Evaluate(_testVectorSecretKeyServer, _testVectorBlindedElement).ToArray();

        // [When("")]
        // public void When()
        // {
        // }

        #endregion

        #region Then

        [Then("the generated byte array is of the length for a group element")]
        public void ThenTheGeneratedByteArrayIsOfTheLengthForAGroupElement()
            => LogAssert.AreEqual("Byte array has expected length", _expectedLength, _actualBytes.Length);

        [Then("the generated byte array contains non zero byte values")]
        public void ThenTheGeneratedByteArrayContainsNonZeroByteValues()
        {
            byte[] allZeroes = Enumerable.Repeat<byte>(0, _expectedLength).ToArray();
            byte[] all255s = Enumerable.Repeat<byte>(255, _expectedLength).ToArray();

            LogAssert.IsFalse("Byte array is not all byte 0", _actualBytes.SequenceEqual(allZeroes));
            LogAssert.IsFalse("Byte array is not all byte 255", _actualBytes.SequenceEqual(all255s));
        }

        [Then("the Server Context evaluation result is correct")]
        public void ThenTheServerContextEvaluationResultIsCorrect()
            => LogAssert.IsTrue("ServerContext Evaluation is correct", _testVectorEvaluationElement.SequenceEqual(_actualBytes));

        // [Then("")]
        // public void Then()
        // {
        // }

        #endregion
    }
}
