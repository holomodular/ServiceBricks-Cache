using Microsoft.AspNetCore.Mvc;
using ServiceBricks;
using System.Net;

namespace WebApp
{
    public class ProblemDetailsMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<Exception, ProblemDetails>(
                (s, d) =>
                {
                    d.Detail = JsonSerializer.Instance.SerializeObject(s);
                    d.Status = (int)HttpStatusCode.InternalServerError;
                    d.Type = s.GetType().FullName;
                    d.Title = s.Message;
                });
        }
    }
}