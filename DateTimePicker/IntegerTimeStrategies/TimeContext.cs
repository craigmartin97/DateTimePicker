using DateTimePicker.Models;

namespace DateTimePicker.IntegerTimeStrategies
{
    internal class TimeContext
    {
        #region Fields

        private readonly TimeFormatSpecifier _formatSpecifier;
        #endregion

        #region Constructors

        public TimeContext(TimeFormatSpecifier timeFormatSpecifier)
        {
            _formatSpecifier = timeFormatSpecifier;
        }
        #endregion

        public void ExecuteIncrease(int hour, int minute, int second,
            out int updatedHour, out int updatedMinute, out int updatedSecond)
        {
            _formatSpecifier.IncrementStrategy
                .UpdateTime(hour, minute, second, out updatedHour, out updatedMinute, out updatedSecond);
        }
        
        public void ExecuteDecrease(int hour, int minute, int second,
            out int updatedHour, out int updatedMinute, out int updatedSecond)
        {
            _formatSpecifier.DecrementStrategy
                .UpdateTime(hour, minute, second, out updatedHour, out updatedMinute, out updatedSecond);
        }

        public void UpdateTime(int hour, int minute, int second, char number, bool previouslyEnteredNumber,
            out int updatedHour, out int updatedMinute, out int updatedSecond)
        {
            _formatSpecifier.ManuallyUpdateTimeStrategy.UpdateTime(hour, minute, second, number,
                previouslyEnteredNumber,
                out updatedHour, out updatedMinute, out updatedSecond);
        }
    }
}