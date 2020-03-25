using Ackee.Domain.Model.TestUtility.Exceptions;
using FluentAssertions;
using Xunit;

namespace Ackee.Core.Test
{
    public class AckeeExceptionTests
    {
        private readonly string _exceptionMessage;

        public AckeeExceptionTests()
        {
            _exceptionMessage = "test";
        }

        [Fact]
        public void should_construct_with_int_as_code()
        {
            const int exceptionCode = 1;
            var testException = new TestAckeeException(exceptionCode, _exceptionMessage);

            testException.Code.Should().Be(exceptionCode);

            testException.Message.Should().Be(_exceptionMessage);
        }

        [Fact]
        public void should_construct_with_enum_as_code()
        {
            var testException = new TestAckeeException(TestEnumException.Zero,_exceptionMessage);

            testException.Code.Should().Be(0);

            testException.Message.Should().Be(_exceptionMessage);

        }
    }
}
