using System;

namespace DateTimePicker.Exceptions
{
    internal class NullResourceException : Exception
    {
        #region Constructors

        public NullResourceException() : base()
        { }

        public NullResourceException(string message) : base(message)
        { }

        public NullResourceException(string message, Exception innerException) : base(message, innerException)
        { }
        #endregion
    }
}