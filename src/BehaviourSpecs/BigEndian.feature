Feature: Big-Endian

Scenario: Should correctly convert an unsigned integer to an ordinal string primitive (I2OSP) - padding with zeros when the requested output length is larger than needed
When I2OSP is performed with a larger output length than is needed
Then the output byte array is correct and padded with zeros

Scenario: Should correctly convert an unsigned integer to an ordinal string primitive (I2OSP) - no padding when the requested output length is exactly what is needed
When I2OSP is performed with an exactly correct output length
Then the output byte array is correct and is exactly the needed length

Scenario: Should error when attempting to convert an unsigned integer to an ordinal string primitive (I2OSP) - when the requested output length is too small
When I2OSP is performed with an output length that is smaller than needed
Then an error occurs about the output length being too small

Scenario: Should correctly convert multiple unsigned integers to an ordinal string primitive (I2OSP) - into the same given byte array at different offsets
When I2OSP is performed with multiple unsigned integers at different offsets in the same byte array
Then the output byte array is correct and is exactly the needed length

Scenario: Should error when attempting to convert an unsigned integer to an ordinal string primitive (I2OSP) - when the requested output length and offset is too small for the length of the given byte array
When I2OSP is performed with an output length and offset that is smaller than needed for the given byte array
Then an error occurs about the output length being too small


Scenario: Should correctly convert an ordinal string to an unsigned integer primitive (OS2IP) - with an input byte array that is padded with zeros
When OS2IP is performed with a byte array that is padded with zeros
Then the output integer is correct

Scenario: Should correctly convert an ordinal string to an unsigned integer primitive (OS2IP) - with an input byte array that is not padded with zeros
When OS2IP is performed with a byte array that is not padded with zeros
Then the output integer is correct

Scenario: Should correctly convert an ordinal string to an unsigned integer primitive (OS2IP) - to the max size of unsigned int
When OS2IP is performed with a byte array for the max size of an unsigned integer
Then the output integer is correct
