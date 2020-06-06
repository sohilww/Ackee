using System;
using Ackee.Core;
using Ackee.Core.Exceptions;
using Ackee.Domain.Model.TestUtility;
using Ackee.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class CheckTests
    {
        private object _nullObject=null;

        [Fact]
        public void When_object_is_null_it_throws_exception()
        {
            Action action = () => Check.NotNull(_nullObject);

            action.Should().Throw<ArgumentNullAckeeException>();
        }

        [Fact]
        public void When_object_is_null_it_throws_exception_with_parameters_name()
        {
            var parameterName = "name";
            Action action = () => Check.NotNull(_nullObject, "name");

            action.Should().Throw<ArgumentNullAckeeException>().ForField(parameterName);
        }
        [Fact]
        public void When_string_is_empty_it_throws_exception()
        {
            Action action = () => Check.NotNullOrWhiteSpace(string.Empty) ;

            action.Should().Throw<ArgumentNullAckeeException>();
        }
        [Fact]
        public void When_string_is_empty_it_throws_exception_with_parameters_name()
        {
            var parameterName = "name";
            Action action = () => Check.NotNullOrWhiteSpace(string.Empty,parameterName);

            action.Should().Throw<ArgumentNullAckeeException>().ForField(parameterName);
        }
    }
}