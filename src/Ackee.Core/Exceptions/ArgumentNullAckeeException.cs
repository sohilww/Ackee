namespace Ackee.Core.Exceptions
{
    public class ArgumentNullAckeeException : AckeeException
    {
        public ArgumentNullAckeeException()
            : base(1,"Argument has problem")
        {
        }
    }
}