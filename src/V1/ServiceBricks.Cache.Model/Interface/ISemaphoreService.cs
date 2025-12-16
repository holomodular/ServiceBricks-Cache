namespace ServiceBricks.Cache
{
    /// <summary>
    /// This defines methods for the semaphore service
    /// </summary>
    public partial interface ISemaphoreService
    {
        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        bool AcquireLock(IDataTransferObject dataTransferObject);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool AcquireLock(Type type);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        bool AcquireLock(string lockKey);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="lockData"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        bool AcquireLock(string lockKey, string lockData, int timeoutMilliseconds, CancellationToken cancellationToken);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        Task<bool> AcquireLockAsync(IDataTransferObject dataTransferObject);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> AcquireLockAsync(Type type);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        Task<bool> AcquireLockAsync(string lockKey);

        /// <summary>
        /// Acquire a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="lockData"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AcquireLockAsync(string lockKey, string lockData, int timeoutMilliseconds, CancellationToken cancellationToken);

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        void ReleaseLock(IDataTransferObject dataTransferObject);

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        void ReleaseLock(Type type);

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        void ReleaseLock(string lockKey);

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="dataTransferObject"></param>
        /// <returns></returns>
        Task ReleaseLockAsync(IDataTransferObject dataTransferObject);

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task ReleaseLockAsync(Type type);

        /// <summary>
        /// Release a lock
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        Task ReleaseLockAsync(string lockKey);
    }
}