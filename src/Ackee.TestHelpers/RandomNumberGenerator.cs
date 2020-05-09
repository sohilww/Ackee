using System;

namespace Ackee.TestHelpers
{
    public class RandomNumberGenerator<T> : RandomGenerator
    {
        private int _maxTo;

        public RandomNumberGenerator<T> MaxTo(int maxTo)
        {
            _maxTo = maxTo;
            return this;
        }

        public T Digits()
        {
            var stringNumber = ProduceStringRandomCharacters("123456789", _maxTo);
            return (T)Convert.ChangeType(stringNumber,typeof(T));
        }
    }
}