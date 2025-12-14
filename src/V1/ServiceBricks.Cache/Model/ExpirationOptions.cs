namespace ServiceBricks.Cache
{
    public partial class ExpirationOptions
    {
        public ExpirationOptions()
        {
            TimerEnabled = false;
            TimerIntervalMilliseconds = 7000;
            TimerDueMilliseconds = 1000;
        }

        /// <summary>
        /// Determine if the timer is enabled
        /// </summary>
        public virtual bool TimerEnabled { get; set; }

        /// <summary>
        /// The timer interval.
        /// </summary>
        public virtual int TimerIntervalMilliseconds { get; set; }

        /// <summary>
        /// The timer due time (1st run).
        /// </summary>
        public virtual int TimerDueMilliseconds { get; set; }
    }
}