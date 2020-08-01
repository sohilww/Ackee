using Ackee.Domain.Model.TestUtility.Exceptions;
using FluentAssertions;
using Xunit;

namespace Ackee.Core.Test
{
    public class ExceptionHandlerTest
    {
        [Fact]
        public void get_code_from_Exception()
        {
            var handler = new ExceptionHandler(new BcConfig() {Code = 1000});


            var returnCode = handler
                .GetCode(new TestAckeeException(5, ""));

            returnCode.Should().Be(1005);
        }
    }
}