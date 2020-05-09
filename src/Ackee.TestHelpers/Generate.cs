using System;
using System.Linq;

namespace Ackee.TestHelpers
{
    public class Generate
    {
        public static RandomGenerator Random() => new RandomGenerator();
    }

    public class RandomGenerator
    {
        public CharactersRandomGenerator Character() => new CharactersRandomGenerator();

        public RandomDigitInStringGenerator DigitsInStringType() => new RandomDigitInStringGenerator();

        protected string ProduceStringRandomCharacters(string chars, int count)
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, count)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public RandomNumberGenerator<T> Number<T>() => new RandomNumberGenerator<T>();
    }
}