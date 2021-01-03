using MongoDB.Bson.Serialization.Attributes;

namespace ReverseGeoCodeApi.Repository.Models
{
    /// <summary>
    /// The basis that represented for geo json format. 
    /// </summary>
    public class Geometry
    {
        /// <value>The shape type such as Point, Polygon, MultiPolygon.</value>
        [BsonElement("type")] 
        public string Type { get; set; }

        /// <value>The coordinate data</value>
        [BsonElement("coordinates")]
        public object Coordinates { get; set; }
    }
}