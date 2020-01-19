namespace Ackee.Domain.Model.Utility
{
    public class IBANValueObject : ValueObject
    {
        public IBANValueObject(string ibanCode)
        {
            IbanCode = ibanCode;
        }

        public string IbanCode { get;private set; }
    }
}