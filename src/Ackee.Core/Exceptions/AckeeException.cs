using System;

namespace Ackee.Core.Exceptions
{
    public abstract class AckeeException : Exception
    {
        protected AckeeException(int code,string message):base(message)
        {
            Code = code;
        }

        public int Code { get;private set; }
    }
}