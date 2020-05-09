namespace Ackee.TestHelpers
{
    public class CharactersRandomGenerator : RandomGenerator
    {
        public string WithLength(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return ProduceStringRandomCharacters(chars, length);
        }
    }
}