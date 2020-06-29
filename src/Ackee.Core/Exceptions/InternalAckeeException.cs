using System;

namespace Ackee.Core.Exceptions
{
    public abstract class InternalAckeeException:AckeeException,IInternalException
    {
        protected InternalAckeeException(int code, string message) : base(code, message)
        {
        }

        protected InternalAckeeException(Enum code, string message) : base(code, message)
        {
        }
    }
}