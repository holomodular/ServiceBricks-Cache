﻿using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// The CacheData domain object.
    /// </summary>
    public partial class CacheData : EntityFrameworkCoreDomainObject<CacheData>, IDpCreateDate, IDpUpdateDate
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public string CacheValue { get; set; }

        /// <summary>
        /// The creation date.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The update date.
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// The expiration date.
        /// </summary>
        public DateTimeOffset? ExpirationDate { get; set; }

        /// <summary>
        /// Provide an expression that will filter an object based on its primary key.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Expression<Func<CacheData, bool>> DomainGetItemFilter(CacheData obj)
        {
            return x => x.CacheKey == obj.CacheKey;
        }
    }
}