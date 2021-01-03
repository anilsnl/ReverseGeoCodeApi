namespace ReverseGeoCode.Models.Location
{
    /// <summary>
    /// The basic response model for Location info.
    /// </summary>
    public class Location
    {
        /// <value>The neighbourhood tha user in.</value>
        public string Neighbourhood { get; set; }

        /// <value>The city tha user in.</value>
        public string City { get; set; }

        /// <value>The district tha user in.</value>
        public string District { get; set; }
        

        /// <value>The unique neighbourhood id.</value>
        public int NeighbourhoodId { get; set; }

        /// <value>The unique district id.</value>
        public int DistrictId { get; set; }

        /// <value>The unique city id.</value>
        public int CityId { get; set; }
    }
}