namespace ServiceBricks.Cache
{
    /// <summary>
    /// This will allow only one server to process a task as the number of application instances are increased.
    /// </summary>
    public partial interface ISingleServerProcessService
    {
        /// <summary>
        /// The heartbeat timeout.
        /// </summary>
        TimeSpan HeartbeatTimeout { get; set; }

        /// <summary>
        /// Determines if this server should run for the given process.
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        Task<bool> ShouldThisServerRunForProcessAsync(string processName);
    }
}