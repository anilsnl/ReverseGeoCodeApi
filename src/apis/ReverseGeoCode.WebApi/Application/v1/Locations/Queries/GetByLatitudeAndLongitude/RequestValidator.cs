using FluentValidation;

namespace ReverseGeoCode.WebApi.Application.v1.Locations.Queries.GetByLatitudeAndLongitude
{
    /// <summary>
    /// Validator for <see cref="Request"/>
    /// <seealso cref="AbstractValidator{T}"/>
    /// </summary>
    public class RequestValidator : AbstractValidator<Request>
    {
        /// <summary>
        /// Init new instance of <see cref="RequestHandler"/>
        /// </summary>
        public RequestValidator()
        {
            RuleFor(a => a.Latitude)
                .NotNull()
                .GreaterThanOrEqualTo(-90)
                .LessThanOrEqualTo(90);

            RuleFor(a => a.Longitude)
                .NotNull()
                .GreaterThanOrEqualTo(-180)
                .LessThanOrEqualTo(180);
        }
    }
}