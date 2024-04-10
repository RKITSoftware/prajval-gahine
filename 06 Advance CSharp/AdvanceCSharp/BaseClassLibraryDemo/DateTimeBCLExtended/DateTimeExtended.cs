namespace BaseClassLibraryDemo.DateTimeBCLExtended
{
    /// <summary>
    /// DateTime Extended feature class
    /// </summary>
    internal class DateTimeExtended
    {
        /// <summary>
        /// Date Time object
        /// </summary>
        private DateTime _dateTime;

        /// <summary>
        /// Date Time property for getting value publicly
        /// </summary>
        public DateTime dateTime
        {
            get
            {
                return _dateTime;
            }
        }

        /// <summary>
        /// Constructor to create DateTimeExtended object with DateTime dependency
        /// </summary>
        /// <param name="dateTime"></param>
        public DateTimeExtended(DateTime dateTime)
        {
            this._dateTime = dateTime;
        }

        /// <summary>
        /// Hour of date time object
        /// </summary>
        public int Hour
        {
            get
            {
                return _dateTime.Hour;
            }
            set
            {
                _dateTime = _dateTime.Subtract(new TimeSpan(_dateTime.Hour, 0, 0));
                _dateTime = _dateTime.AddHours(value);
            }
        }

        /// <summary>
        /// Minute of date time object
        /// </summary>
        public int Minute
        {
            get
            {
                return _dateTime.Minute;
            }
            set
            {
                _dateTime = _dateTime.Subtract(new TimeSpan(0, _dateTime.Minute, 0));
                _dateTime = _dateTime.AddHours(value);
            }
        }

        /// <summary>
        /// Second of date time object
        /// </summary>
        public int Second
        {

            get
            {
                return _dateTime.Second;
            }
            set
            {
                _dateTime = _dateTime.Subtract(new TimeSpan(0, 0, _dateTime.Second));
                _dateTime = _dateTime.AddHours(value);
            }
        }

        /// <summary>
        /// Day of date time object
        /// </summary>
        public int Day
        {

            get
            {
                return _dateTime.Day;
            }
            set
            {
                _dateTime = _dateTime.Subtract(new TimeSpan(_dateTime.Day, 0, 0, 0));
                _dateTime = _dateTime.AddHours(value);
            }
        }
    }
}
