using System;
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
    }
}
