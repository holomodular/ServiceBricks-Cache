using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a timer to execute the CacheExpirationTask to expire stale cache data.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public partial class CacheExpirationTimer : TaskTimerHostedService<CacheExpirationTask.Detail, CacheExpirationTask.Worker>
    {
        private ExpirationOptions _expirationOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public CacheExpirationTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger,
            IOptions<ExpirationOptions> options) : base(serviceProvider, logger)
        {
            _expirationOptions = options.Value;
            TimerTickInterval = TimeSpan.FromMilliseconds(_expirationOptions.TimerIntervalMilliseconds);
            TimerDueTime = TimeSpan.FromMilliseconds(_expirationOptions.TimerDueMilliseconds);
        }

        /// <summary>
        /// The task detail for the timer that will be executed.
        /// </summary>
        public override ITaskDetail<CacheExpirationTask.Detail, CacheExpirationTask.Worker> TaskDetail
        {
            get { return new CacheExpirationTask.Detail(); }
        }

        /// <summary>
        /// Determine if the timer should process the run.
        /// </summary>
        /// <returns></returns>
        public override bool TimerTickShouldProcessRun()
        {
            // AI: Check if the module is started and the timer is not currently running.
            return CacheModule.Instance.Started && !IsCurrentlyRunning;
        }
    }
}