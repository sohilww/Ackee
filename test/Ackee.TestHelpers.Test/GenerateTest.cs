using System;
using FluentAssertions;
using Xunit;

namespace Ackee.TestHelpers.Test
{
    public class GenerateTest
    {
        [Fact]
        public void Generate_Random_String()
        {
            var length = 15;

            var randomString = Generate.Random().Character().WithLength(length);

            randomString.Should().HaveLength(length);
        }

        [Fact]
        public void Generate_Random_Digits_In_String_Type()
        {
            var length = 15;

            var randomString = Generate.Random().DigitsInStringType().WithLength(length);

            randomString.Should().HaveLength(length);
            randomString.Should().BeNumber();
        }

        [Fact]
        public void Generate_Random_Number()
        {
            var digits = Generate.Random().Number<int>().MaxTo(5).Digits();

            digits.Should().BeOfType(typeof(int));
        }
    }
}
