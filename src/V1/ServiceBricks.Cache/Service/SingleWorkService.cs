using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceQuery;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// This is a service for processing a domain object table like a queue.
    /// </summary>
    /// <typeparam name="TDomainObject"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract partial class SingleWorkService<TDto> : WorkService<TDto>
        where TDto : class, IDataTransferObject, IDpWorkProcess
    {
        protected readonly ISingleServerProcessService _singleServerProcessService;

        /// <summary>
        /// Consrtuctor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="apiService"></param>
        /// <param name="singleServerProcessorService"></param>
        public SingleWorkService(
            ILoggerFactory loggerFactory,
            IApiService<TDto> apiService,
            ISingleServerProcessService singleServerProcessService) : base(loggerFactory, apiService)
        {
            _singleServerProcessService = singleServerProcessService;
        }

        /// <summary>
        /// Execute the process.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (await _singleServerProcessService.ShouldThisServerRunForProcessAsync(this.GetType().AssemblyQualifiedName))
                await base.ExecuteAsync(cancellationToken);
        }
    }
}