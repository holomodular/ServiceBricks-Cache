using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Cache.MongoDb
{
    /// <summary>
    /// This is persisted key/value data.
    /// </summary>
    public partial class CacheData : MongoDbDomainObject<CacheData>, IDpCreateDate, IDpUpdateDate
    {
        public CacheData()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public virtual string Key { get; set; }

        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset UpdateDate { get; set; }
        public virtual DateTimeOffset? ExpirationDate { get; set; }
        public virtual string Value { get; set; }

        public override Expression<Func<CacheData, bool>> DomainGetItemFilter(CacheData obj)
        {
            return x => x.Id == obj.Id;
        }
    }
}