using System;

namespace Ackee.Core.Exceptions
{
    public abstract class AckeeException : Exception
    {
        protected AckeeException(int code,string message):base(message)
        {
            Code = code;
        }

        protected AckeeException(Enum code,string message):this(1,message)
        {
            
        }

        public int Code { get;private set; }
    }
}