using System;
using System.Threading.Tasks;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This will allow only one server to process a task
    /// as the number of application instances are increased.
    /// </summary>
    public interface ISingleServerProcessService
    {
        TimeSpan HeartbeatTimeout { get; set; }

        Task<bool> ShouldThisServerRunForProcessAsync(string processName);
    }
}