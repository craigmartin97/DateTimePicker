namespace DateTimePicker.Models
{
    public class Time
    {
        #region Properties

        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public string Value { get; set; }
        #endregion

        #region Constructors

        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;

            string hourStr = Hour < 10 ? $"0{Hour}" : Hour.ToString();
            string minuteStr = Minute < 10 ? $"0{Minute}" : Minute.ToString();

            Value = $"{hourStr}:{minuteStr}";
        }

        public Time(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;

            string hourStr = Hour < 10 ? $"0{Hour}" : Hour.ToString();
            string minuteStr = Minute < 10 ? $"0{Minute}" : Minute.ToString();
            string secondStr = Second < 10 ? $"0{Second}" : Second.ToString();

            Value = $"{hourStr}:{minuteStr}:{secondStr}";
        }
        #endregion

        public override string ToString() => Value;
    }
}