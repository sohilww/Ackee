namespace Ackee.Domain.Model.TestUtility
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