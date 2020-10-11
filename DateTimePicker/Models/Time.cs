namespace DateTimePicker.Models
{
    internal class Time
    {
        #region Properties

        public int Id { get; set; }
        public string Value { get; set; }
        #endregion

        #region Constructors

        public Time(int id, string value)
        {
            Id = id;
            Value = value;
        }
        #endregion

        public override string ToString()
        {
            return Value;
        }
    }
}