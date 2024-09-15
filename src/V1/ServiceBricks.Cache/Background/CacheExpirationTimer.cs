﻿using Microsoft.Extensions.Logging;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a timer to execute the CacheExpirationTask to expire stale cache data.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public partial class CacheExpirationTimer : TaskTimerHostedService<CacheExpirationTask.Detail, CacheExpirationTask.Worker>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public CacheExpirationTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        /// <summary>
        /// The interval at which the timer will tick.
        /// </summary>
        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromMinutes(5); }
        }

        /// <summary>
        /// The initial delay before the timer will tick.
        /// </summary>
        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromSeconds(1); }
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
            return ApplicationBuilderExtensions.ModuleStarted && !IsCurrentlyRunning;
        }
    }
}