using System;
using System.Text.RegularExpressions;
using Ackee.Core.Exceptions;
using FluentAssertions;
using FluentAssertions.Primitives;
using FluentAssertions.Specialized;

namespace Ackee.TestHelpers
{
    public static class ExceptionAssertionCustom
    {
        public static AndConstraint<StringAssertions> ForField<T>(this ExceptionAssertions<T> exception,
            string fieldName) where T : AckeeException
        {
            fieldName = fieldName.ToLower();
            return exception.And.Message.ToLower().Should().Contain(fieldName);
        }

        public static void BeNumber(this StringAssertions value)
        {
           Regex regex = new Regex("^[0-9]+$");
           regex.IsMatch(value.Subject).Should().BeTrue();
        }
    }
}
