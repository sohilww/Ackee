using System;
using Ackee.Core.Exceptions;

namespace Ackee.Domain.Model.TestUtility.Exceptions
{
    public class TestAckeeException :AckeeException
    {
        public TestAckeeException(int code, string message) : base(code, message)
        {
        }

        public TestAckeeException(Enum code, string message)
            : base(code, message)
        {
        }
    }
}