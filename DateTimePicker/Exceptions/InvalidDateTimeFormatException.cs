using System;

namespace DateTimePicker.Exceptions
{
    internal class InvalidDateTimeFormatException : Exception
    {
        #region Constructors

        public InvalidDateTimeFormatException() : base()
        { }

        public InvalidDateTimeFormatException(string message) : base(message)
        { }

        public InvalidDateTimeFormatException(string message, Exception innerException) : base(message, innerException)
        { }
        #endregion
    }
}