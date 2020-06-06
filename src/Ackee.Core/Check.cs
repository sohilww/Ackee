using System.Collections.Generic;
using Ackee.Core.Exceptions;
using Ackee.Extensions;

namespace Ackee.Core
{
    public class Check
    {
        public static T NotNull<T>(T value, string parameterName="")
        {
            if (value == null)
                throw new ArgumentNullAckeeException(parameterName);
            return value;
        }
        public static string NotNullOrWhiteSpace(string value, string parameterName="")
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullAckeeException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            return value;
        }

        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentNullAckeeException(parameterName + " can not be null or empty!", parameterName);
            }
            return value;
        }
    }
}