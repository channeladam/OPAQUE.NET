#nullable disable

using System;
using System.Linq;
using ChannelAdam.TestFramework.NUnit.Abstractions;
using Opaque.Net.Internal;
using TechTalk.SpecFlow;

namespace BehaviourSpecs
{
    [Binding]
    [Scope(Feature = "Big-Endian")]
    public class BigEndianUnitTests : TestEasy
    {
        private byte[] _expectedBytes;
        private byte[] _actualBytes;
        private uint _expectedInt;
        private uint _actualInt;

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

        [When("I2OSP is performed with a larger output length than is needed")]
        public void WhenI2OSPIsPerformedWithALargerOutputLengthThanIsNeeded()
        {
            _actualBytes = BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(944, 3).ToArray();
            _expectedBytes = new byte[] { 0, 3, 176 }; // 944 in base 256
        }

        [When("I2OSP is performed with an exactly correct output length")]
        public void WhenI2OSPIsPerformedWithAnExactlyCorrectOutputLength()
        {
            _actualBytes = BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(944, 2).ToArray();
            _expectedBytes = new byte[] { 3, 176 }; // 944 in base 256
        }

        [When("I2OSP is performed with an output length that is smaller than needed")]
        public void WhenI2OSPIsPerformedWithAnOutputLengthThatIsSmallerThanNeeded()
        {
            ExpectedException.ExpectedType = typeof(ArithmeticException);
            ExpectedException.MessageShouldContainText = "too large";
            Try(() => BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(944, 1));
        }

        [When("I2OSP is performed with multiple unsigned integers at different offsets in the same byte array")]
        public void WhenI2OSPIsPerformedWithMultipleUnsignedIntegersAtDifferentOffsetsInTheSameByteArray()
        {
            _actualBytes = new byte[5];
            BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(944, 2, ref _actualBytes, 3); // offset 3 (positions 4 & 5)
            BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(257, 2, ref _actualBytes, 0); // offset 0
            _expectedBytes = new byte[] { 1, 1, 0, 3, 176 }; // 257 then 0 then 944 in base 256
        }

        [When("I2OSP is performed with an output length and offset that is smaller than needed for the given byte array")]
        public void WhenI2OSPIsPerformedWithAnOutputLengthAndOffsetThatIsSmallerThanNeededForTheGivenByteArray()
        {
            ExpectedException.ExpectedType = typeof(ArithmeticException);
            ExpectedException.MessageShouldContainText = "too large";
            byte[] result = new byte[3];
            Try(() => BigEndianUtils.ConvertIntegerToOrdinalStringPrimitive(944, 2, ref result, 2));
        }

        [When("OS2IP is performed with a byte array that is padded with zeros")]
        public void WhenOS2IPIsPerformedWithAByteArrayThatIsPaddedWithZeros()
        {
            byte[] bytes = { 0, 0, 3, 176 }; // 944 in base 256
            _expectedInt = 944;
            _actualInt = BigEndianUtils.ConvertOrdinalStringToIntegerPrimitive(bytes);
        }

        [When("OS2IP is performed with a byte array that is not padded with zeros")]
        public void WhenOS2IPIsPerformedWithAByteArrayThatIsNotPaddedWithZeros()
        {
            byte[] bytes = { 3, 176 }; // 944 in base 256
            _expectedInt = 944;
            _actualInt = BigEndianUtils.ConvertOrdinalStringToIntegerPrimitive(bytes);
        }

        [When("OS2IP is performed with a byte array for the max size of an unsigned integer")]
        public void WhenOS2IPIsPerformedWithAByteArrayForTheMaxSizeOfAnUnsignedInteger()
        {
            byte[] bytes = { 255, 255, 255, 255 }; // max in base 256
            _expectedInt = uint.MaxValue;
            _actualInt = BigEndianUtils.ConvertOrdinalStringToIntegerPrimitive(bytes);
        }

        // [When("")]
        // public void When()
        // {
        // }

        #endregion

        #region Then

        [Then("the output byte array is correct and padded with zeros")]
        [Then("the output byte array is correct and is exactly the needed length")]
        public void ThenTheOutputByteArrayIsCorrect()
            => LogAssert.IsTrue("Output byte array is as expected", _expectedBytes.SequenceEqual(_actualBytes));

        [Then("an error occurs about the output length being too small")]
        public void ThenAnErrorOccursAboutTheOutputLengthBeingTooSmall()
            => AssertExpectedException();

        [Then("the output integer is correct")]
        public void ThenTheOutputIntegerIsCorrect()
            => LogAssert.AreEqual("Output Integer from OS2IP", _expectedInt, _actualInt);

        // [Then("")]
        // public void Then()
        // {
        // }

        #endregion
    }
}
