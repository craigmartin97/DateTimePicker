using System;

namespace DateTimePicker
{
    internal class GetDateTime
    {
        public static DateTime? GetDateTimeFromString(string text)
        {
            bool isValidDateTime = DateTime.TryParse(text, out DateTime dateTime);
            return !isValidDateTime ? (DateTime?)null : dateTime;
        }
    }
}