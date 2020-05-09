namespace Ackee.TestHelpers
{
    public class RandomDigitInStringGenerator : RandomGenerator
    {
        public string WithLength(int count)
        {
            const string chars = "0123456789";
            return ProduceStringRandomCharacters(chars, count);
        }
    }
}