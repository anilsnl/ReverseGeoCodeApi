using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReverseGeoCode.WebApi.Controllers.v1
{
    /// <summary>
    /// This controller contains location related actions.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LocationsController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Init new instance of <see cref="LocationsController"/>
        /// </summary>
        /// <param name="mediator"></param>
        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Finds the location by using reverse geo coding.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getByLatitudeAndLongitude")]
        public async Task<IActionResult> Get(
            [FromQuery] Application.v1.Locations.Queries.GetByLatitudeAndLongitude.Request request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}