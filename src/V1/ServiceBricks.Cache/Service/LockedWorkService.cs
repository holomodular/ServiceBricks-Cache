using Microsoft.Extensions.Logging;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This will obtain a semaphore lock so other processes cannot access the table at the same time.
    /// </summary>
    /// <typeparam name="TDomainObject"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract partial class LockedWorkService<TDto> : WorkService<TDto>
        where TDto : class, IDataTransferObject, IDpWorkProcess
    {
        protected readonly ISemaphoreService _semaphoreService;

        /// <summary>
        /// Consrtuctor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="apiService"></param>
        /// <param name="semaphoreService"></param>
        public LockedWorkService(
            ILoggerFactory loggerFactory,
            IApiService<TDto> apiService,
            ISemaphoreService semaphoreService) : base(loggerFactory, apiService)
        {
            _semaphoreService = semaphoreService;
            SemaphoreLockName = this.GetType().AssemblyQualifiedName;
        }

        /// <summary>
        /// The name of the semaphore lock name used for locking.
        /// </summary>
        public virtual string SemaphoreLockName { get; set; }

        /// <summary>
        /// Get the list of queue items to process
        /// </summary>
        /// <param name="batchNumberToTake"></param>
        /// <param name="pickupErrors"></param>
        /// <param name="errorPickupCutoffDate"></param>
        /// <returns></returns>
        public override async Task<IResponseList<TDto>> GetQueueItemsAsync(int batchNumberToTake, bool pickupErrors, DateTimeOffset errorPickupCutoffDate)
        {
            // Lock this object type so other types won't touch records
            var lockAcquired = await _semaphoreService.AcquireLockAsync(SemaphoreLockName);
            if (lockAcquired)
            {
                var resp = await base.GetQueueItemsAsync(batchNumberToTake, pickupErrors, errorPickupCutoffDate);
                await _semaphoreService.ReleaseLockAsync(SemaphoreLockName);
                return resp;
            }
            return new ResponseList<TDto>();
        }
    }
}