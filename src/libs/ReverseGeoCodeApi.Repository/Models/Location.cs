using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReverseGeoCodeApi.Repository.Models
{
    /// <summary>
    /// Mongo db document model for location.
    /// </summary>
    [BsonIgnoreExtraElements]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Location
    {
        /// <value>id parameter <seealso cref="ObjectId"/></value>
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        /// <value>neighbourhood</value>

        [BsonElement("neighbourhood")]
        public string Neighbourhood { get; set; }

        /// <value>city</value>

        [BsonElement("city")]
        public string City { get; set; }

        /// <value>district</value>

        [BsonElement("district")]
        public string District { get; set; }

        /// <value>city id</value>

        [BsonElement("city_id")]
        public int CityId { get; set; }

        /// <value>neighbourhood id</value>
        [BsonElement("neighbourhood_id")]
        public int NeighbourhoodId { get; set; }

        /// <value>District id.</value>

        [BsonElement("district_id")]
        public int DistrictId { get; set; }


        /// <value>The geo data.</value>

        [BsonElement("geometry")]
        public Geometry Geometry { get; set; }
    }
}