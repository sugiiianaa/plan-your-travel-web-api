using MediatR;
using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Flights.Queries.GetFlightSchedule
{
    public sealed class GetFlightScheduleQueryHandler
        : IRequestHandler<GetFlightScheduleQuery, Result<IList<FlightScheduleDto>>>
    {
        private readonly IFlightRepository _flightRepository;

        public GetFlightScheduleQueryHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Result<IList<FlightScheduleDto>>> Handle(
            GetFlightScheduleQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var schedule = await _flightRepository
                    .GetFlightSchedule(request.DepartureDate,
                        request.DepartureAirportId,
                        request.ArrivalAirportId,
                        cancellationToken);

                return Result.Success<IList<FlightScheduleDto>>(schedule);
            }
            catch (Exception)
            {
                return Result.Failure<IList<FlightScheduleDto>>(DomainErrors.Generic.InternalServerError);
            }
            throw new NotImplementedException();
        }
    }
}
