using Microsoft.Extensions.Options;

namespace ServiceBricks.Cache
{
    public partial class SemaphoreService : ISemaphoreService
    {
        protected readonly ICacheDataApiService _cacheDataApiService;
        protected readonly SemaphoreOptions _semaphoreOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheDataApiService"></param>
        /// <param name="semaphoreOptions"></param>
        public SemaphoreService(
            ICacheDataApiService cacheDataApiService,
            IOptions<SemaphoreOptions> semaphoreOptions)
        {
            _cacheDataApiService = cacheDataApiService;
            _semaphoreOptions = semaphoreOptions.Value;
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        public virtual bool AcquireLock(IDataTransferObject dataTransferObject)
        {
            string lockKey = GetLockKey(dataTransferObject);
            CancellationTokenSource cts = new CancellationTokenSource(_semaphoreOptions.CancellationMilliseconds);
            return AcquireLock(
                lockKey,
                null,
                _semaphoreOptions.OrphanTimeoutMilliseconds,
                cts.Token);
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool AcquireLock(Type type)
        {
            string lockKey = type.AssemblyQualifiedName;
            CancellationTokenSource cts = new CancellationTokenSource(_semaphoreOptions.CancellationMilliseconds);
            return AcquireLock(
                lockKey,
                null,
                _semaphoreOptions.OrphanTimeoutMilliseconds,
                cts.Token);
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        public virtual bool AcquireLock(string lockKey)
        {
            CancellationTokenSource cts = new CancellationTokenSource(_semaphoreOptions.CancellationMilliseconds);
            return AcquireLock(
                lockKey,
                null,
                _semaphoreOptions.OrphanTimeoutMilliseconds,
                cts.Token);
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="expiration"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual bool AcquireLock(string lockKey, string lockData, int timeoutMilliseconds, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var respCreate = _cacheDataApiService.Create(new CacheDataDto()
                    {
                        CacheKey = lockKey,
                        CacheValue = lockData,
                        ExpirationDate = DateTimeOffset.UtcNow.AddMilliseconds(timeoutMilliseconds)
                    });
                    if (respCreate.Success)
                        return true;

                    Thread.Sleep(_semaphoreOptions.DelayMilliseconds);
                }

                return false;
            }
            catch
            {
                // No need to log this
                return false;
            }
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        public virtual async Task<bool> AcquireLockAsync(IDataTransferObject dataTransferObject)
        {
            string lockKey = GetLockKey(dataTransferObject);
            CancellationTokenSource cts = new CancellationTokenSource(_semaphoreOptions.CancellationMilliseconds);
            return await AcquireLockAsync(
                lockKey,
                null,
                _semaphoreOptions.OrphanTimeoutMilliseconds,
                cts.Token);
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        public virtual async Task<bool> AcquireLockAsync(Type type)
        {
            string lockKey = type.AssemblyQualifiedName;
            CancellationTokenSource cts = new CancellationTokenSource(_semaphoreOptions.CancellationMilliseconds);
            return await AcquireLockAsync(
                lockKey,
                null,
                _semaphoreOptions.OrphanTimeoutMilliseconds,
                cts.Token);
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        public virtual async Task<bool> AcquireLockAsync(string lockKey)
        {
            CancellationTokenSource cts = new CancellationTokenSource(_semaphoreOptions.CancellationMilliseconds);
            return await AcquireLockAsync(
                lockKey,
                null,
                _semaphoreOptions.OrphanTimeoutMilliseconds,
                cts.Token);
        }

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="lockData"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> AcquireLockAsync(string lockKey, string lockData, int timeoutMilliseconds, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var respCreate = await _cacheDataApiService.CreateAsync(new CacheDataDto()
                    {
                        CacheKey = lockKey,
                        CacheValue = lockData,
                        ExpirationDate = DateTimeOffset.UtcNow.AddMilliseconds(timeoutMilliseconds)
                    });
                    if (respCreate.Success)
                        return true;

                    await Task.Delay(_semaphoreOptions.DelayMilliseconds, cancellationToken);
                }

                return false;
            }
            catch
            {
                // No need to log this
                return false;
            }
        }

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        public virtual void ReleaseLock(IDataTransferObject dataTransferObject)
        {
            _cacheDataApiService.Delete(GetLockKey(dataTransferObject));
        }

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual void ReleaseLock(Type type)
        {
            _cacheDataApiService.Delete(type.AssemblyQualifiedName);
        }

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        public virtual void ReleaseLock(string lockKey)
        {
            _cacheDataApiService.Delete(lockKey);
        }

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        public virtual async Task ReleaseLockAsync(IDataTransferObject dataTransferObject)
        {
            await _cacheDataApiService.DeleteAsync(GetLockKey(dataTransferObject));
        }

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual async Task ReleaseLockAsync(Type type)
        {
            await _cacheDataApiService.DeleteAsync(type.AssemblyQualifiedName);
        }

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        public virtual async Task ReleaseLockAsync(string lockKey)
        {
            await _cacheDataApiService.DeleteAsync(lockKey);
        }

        /// <summary>
        /// Get the lock key for a DTO
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        protected virtual string GetLockKey(IDataTransferObject dataTransferObject)
        {
            if (dataTransferObject == null)
                return null;
            return dataTransferObject.GetType().AssemblyQualifiedName + ":" + dataTransferObject.StorageKey;
        }
    }
}