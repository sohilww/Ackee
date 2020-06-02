using System;

namespace Ackee.Core.Exceptions
{
    public abstract class AckeeException : Exception
    {
        protected AckeeException(int code,string message):base(message)
        {
            Code = code;
        }

        protected AckeeException(Enum code,string message):this(Convert.ToInt32(code),message)
        {
            
        }

        public int Code { get;private set; }
    }
}