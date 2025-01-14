using MediatR;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSeatClass
{
    public class CreateFlightSeatClassCommandHandler : IRequestHandler<CreateFlightSeatClassCommand, Result<List<Guid>
    {
        private readonly IFlightRepository _flightRepository;

        public CreateFlightSeatClassCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Result<List<Guid>>> Handle(CreateFlightSeatClassCommand request, CancellationToken cancellationToken)
        {
            var createdIds = new List<Guid>();


            using (var transaction = await _flightRepository.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    // Process each seat class in the batch
                    foreach (var seatClassDto in request.FlightSeatClass)
                    {
                        // 1. Retrieve aggregate root
                        var flightSchedule = await _flightRepository
                            .GetFlightShcheduleById(seatClassDto.FlightScheduleId);

                        // Fail fast: if flight schedule not found, stop everything
                        if (flightSchedule is null)
                        {
                            // Rollback & return a failure
                            await transaction.RollbackAsync(cancellationToken);
                            return Result.Fail<List<Guid>>(
                                $"FlightSchedule not found for ID {seatClassDto.FlightScheduleId}");
                        }

                        // 2. Use a domain method to add the seat class 
                        //    (Domain validation may throw a DomainException)
                        var seatClass = flightSchedule.AddSeatClass(
                            Guid.NewGuid(),
                            seatClassDto.Type,
                            seatClassDto.Capacity,
                            seatClassDto.Price
                        );

                        createdIds.Add(seatClass.Id);

                        // 3. Update the flight schedule in the repository
                        await _flightRepository.UpdateAsync(flightSchedule);
                    }

                    // 4. Persist changes (all seat classes at once)
                    await _flightRepository.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);

                    // If we got here, it means everything succeeded
                    return Result.Success(createdIds);
                }
                catch (DomainException dex)
                {
                    // Handle domain-level validation errors, rollback & fail
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Fail<List<Guid>>(dex.Message);
                }
                catch (Exception ex)
                {
                    // Handle unexpected errors, rollback & fail
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Fail<List<Guid>>(
                        "An unexpected error occurred: " + ex.Message);
                }
            }
        }
    }
}
