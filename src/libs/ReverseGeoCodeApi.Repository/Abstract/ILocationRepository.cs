using System.Threading.Tasks;
using ReverseGeoCodeApi.Repository.Models;

namespace ReverseGeoCodeApi.Repository.Abstract
{
    /// <summary>
    /// Repository for <see cref="Location"/> model.
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Determines the matching location with the passing <paramref name="latitude"/>
        /// and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude for checking.</param>
        /// <param name="longitude">The longitude for checking.</param>
        /// <returns>The <see cref="Location"/> model with <see cref="Task{TResult}"/></returns>
        Task<Location> GetLocationInByLatitudeAndLongitudeAsync(double latitude, double longitude);
        
        
        /// <summary>
        /// Determines the matching location with the passing <paramref name="latitude"/>
        /// and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The latitude for checking.</param>
        /// <param name="longitude">The longitude for checking.</param>
        /// <returns>The <see cref="Location"/></returns>
        Location GetLocationInByLatitudeAndLongitude(double latitude, double longitude);

        /// <summary>
        /// Creates geometry index.
        /// </summary>
        /// <returns>The mame of the index.</returns>
        Task<string> EnsureIndexCreatedAsync();
    }
}