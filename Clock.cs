using System;

namespace Assignment_06
{
    /// <summary>
    /// Class <c>Clock</c> is used to produce a string representation of a clock.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The clock string can be produced either by using the method <c>UpdateClock</c>
    /// which gets the current time from <c>System.DateTime.Now</c>, or by using the
    /// method <c>UpdateHomeMadeClock</c> which only synchronizes against the system's
    /// clock once, and then simply increments time.
    /// </para>
    /// <para>
    /// The code in the constructor is only needed for the home-made clock.
    /// </para>
    /// </remarks>
    internal class Clock
    {
        DateTime now;

        private string seconds;
        private string minutes;
        private string hours;

        // Only needed for the home-made clock
        private int timerSeconds;
        private int timerMinutes;
        private int timerHours;

        public Clock()
        {
            InitializeHomeMadeClock();
        }

        /// <summary>
        /// Initializes the home made clock.
        /// </summary>
        private void InitializeHomeMadeClock()
        {
            now = System.DateTime.Now;
            timerSeconds = now.Second;
            timerMinutes = now.Minute;
            timerHours = now.Hour;
            seconds = timerSeconds.ToString().PadLeft(2, '0');
            minutes = timerMinutes.ToString().PadLeft(2, '0');
            hours = timerHours.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Method <c>UpdateClock</c> formats a string for use as a clock, containing the 
        /// current time as provided by DateTime.
        /// </summary>
        /// <returns>A string representation of a clock.</returns>
        public string UpdateClock()
        {
            now = System.DateTime.Now;

            seconds = now.Second.ToString().PadLeft(2, '0');
            minutes = now.Minute.ToString().PadLeft(2, '0');
            hours = now.Hour.ToString().PadLeft(2, '0');

            return $"{hours}:{minutes}:{seconds}";
        }

        /// <summary>
        /// Method <c>UpdateHomeMadeClock</c> formats a string for use as a clock.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This clock uses the system's time to set itself only when the constructor
        /// of this class is executed, then just increments seconds, minutes and hours.
        /// </para>
        /// <para>
        /// This clock doesn't really know what the time is.
        /// </para>
        /// </remarks>
        /// <returns>A string representation of a clock.</returns>
        public string UpdateHomeMadeClock()
        {
            timerSeconds++;
            seconds = timerSeconds.ToString().PadLeft(2, '0');

            if (timerSeconds == 60)
            {
                timerSeconds = 0;
                timerMinutes++;
                minutes = timerMinutes.ToString().PadLeft(2, '0');

                if (timerMinutes == 60)
                {
                    timerMinutes = 0;
                    timerHours++;
                    hours = timerHours.ToString().PadLeft(2, '0');

                    if (timerHours == 25)
                    {
                        timerHours = 0;
                        hours = timerHours.ToString().PadLeft(2, '0');
                    }
                }
            }
            return $"{hours}:{minutes}:{seconds}";
        }
    }
}
