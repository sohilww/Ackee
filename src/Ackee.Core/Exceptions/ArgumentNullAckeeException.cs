namespace Ackee.Core.Exceptions
{
    public class ArgumentNullAckeeException : AckeeException,IInternalException
    {
        public ArgumentNullAckeeException(string field="")
            : base(1,$"Argument {field} has problem")
        {
        }

        public ArgumentNullAckeeException(string message,string field) 
            :base(1,message)
        {
            
        }
    }
}