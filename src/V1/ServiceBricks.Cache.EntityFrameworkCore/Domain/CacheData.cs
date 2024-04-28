using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Cache.EntityFrameworkCore
{
    /// <summary>
    /// This is persisted key/value data.
    /// </summary>
    public partial class CacheData : EntityFrameworkCoreDomainObject<CacheData>, IDpCreateDate, IDpUpdateDate
    {
        public CacheData()
        {
        }

        public string Key { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Value { get; set; }

        public override Expression<Func<CacheData, bool>> DomainGetItemFilter(CacheData obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}