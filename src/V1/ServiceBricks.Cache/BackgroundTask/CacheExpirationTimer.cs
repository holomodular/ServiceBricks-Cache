using Microsoft.Extensions.Logging;
using System;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a timer to execute the background task to expire cache data.
    /// </summary>
    public class CacheExpirationTimer : TaskTimerHostedService<CacheExpirationTask.Detail, CacheExpirationTask.Worker>
    {
        public CacheExpirationTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromMinutes(30); }
        }

        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromMinutes(30); }
        }

        public override ITaskDetail<CacheExpirationTask.Detail, CacheExpirationTask.Worker> TaskDetail
        {
            get { return new CacheExpirationTask.Detail(); }
        }

        public override bool TimerTickShouldProcessRun()
        {
            return ApplicationBuilderExtensions.ModuleStarted && !IsCurrentlyRunning;
        }
    }
}