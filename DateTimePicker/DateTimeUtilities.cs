﻿using System;

namespace DateTimePicker
{
    internal static class DateTimeUtilities
    {
        public static DateTime GetContextNow(DateTimeKind kind)
        {
            if (kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            return (kind == DateTimeKind.Utc)
                ? DateTime.UtcNow
                : DateTime.Now;
        }

        public static bool IsSameDate(DateTime? date1, DateTime? date2)
        {
            if (date1 == null || date2 == null)
                return false;

            return (date1.Value.Date == date2.Value.Date);
        }
    }
}