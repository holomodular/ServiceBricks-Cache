namespace ServiceBricks.Cache
{
    public partial class SemaphoreOptions
    {
        public SemaphoreOptions()
        {
            DelayMilliseconds = 3000;
            CancellationMilliseconds = 20000;
            OrphanTimeoutMilliseconds = 10000;
        }

        /// <summary>
        /// The delay for each loop when calling the api service to obtain a lock if it fails.
        /// </summary>
        public virtual int DelayMilliseconds { get; set; }

        /// <summary>
        /// The total time to wait before the acquire call is cancelled.
        /// </summary>
        public virtual int CancellationMilliseconds { get; set; }

        /// <summary>
        /// The total time before the lock is orphaned and will be cleaned up
        /// </summary>
        public virtual int OrphanTimeoutMilliseconds { get; set; }
    }
}