using System;
using Ackee.Core.Exceptions;

namespace Ackee.AspNetCore
{
    public class InvalidFileException : InternalAckeeException
    {
        public InvalidFileException(string fileName) :
            base(2, $"invalid file: {fileName} file is too large")
        {
        }

        public InvalidFileException(Enum code, string message) : base(code, message)
        {
        }
    }
}