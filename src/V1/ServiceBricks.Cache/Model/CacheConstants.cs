﻿namespace ServiceBricks.Cache
{
    /// <summary>
    /// These are constants for the ServiceBricks Cache module.
    /// </summary>
    public static partial class CacheConstants
    {
        /// <summary>
        /// Application settings keys for client API configuration.
        /// </summary>
        public const string APPSETTING_CLIENT_APICONFIG = @"ServiceBricks:Cache:Client:Api";

        /// <summary>
        /// Application settings keys for semaphore options.
        /// </summary>
        public const string APPSETTING_SEMAPHORE_OPTIONS = "ServiceBricks:Cache:Semaphore";
    }
}