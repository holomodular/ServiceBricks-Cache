namespace ServiceBricks.Cache
{
    public partial class SemaphoreOptions
    {
        public SemaphoreOptions()
        {
            DelayMilliseconds = 300;
            CancellationMilliseconds = 10000;
            OrphanExpirationMilliseconds = 5000;
            ExpirationTimerIntervalMilliseconds = 2500;
        }

        /// <summary>
        /// The delay for each loop when calling the api service to obtain a lock
        /// </summary>
        public virtual int DelayMilliseconds { get; set; } = 300;

        /// <summary>
        /// The total time to wait before the acquire call is cancelled
        /// </summary>
        public virtual int CancellationMilliseconds { get; set; } = 10000;

        /// <summary>
        /// The total time before the lock is orphaned and will be cleaned up
        /// Background timer for expiration runs every 3 seconds
        /// </summary>
        public virtual int OrphanExpirationMilliseconds { get; set; } = 5000;

        /// <summary>
        /// The expiration timer interval.
        /// </summary>
        public virtual int ExpirationTimerIntervalMilliseconds { get; set; } = 2500;
    }
}