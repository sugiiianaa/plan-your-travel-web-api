using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Domain.Entities.Transactions;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Infrastructure.Services.GetCurrentUser;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Queries.GetFlightTransaction
{
    public class GetFlightTransactionQueryHandler(
        IFlightTransactionRepository flightTransactionRepository,
        IUserRepository userRepository,
        IGetCurrentUser getCurrentUser) : IRequestHandler<GetFlightTransactionQuery, Result<PaginatedResultDto<FlightTransactionDto>>>
    {
        private readonly IFlightTransactionRepository _flightTransactionRepository = flightTransactionRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IGetCurrentUser _getCurrentUser = getCurrentUser;

        public async Task<Result<PaginatedResultDto<FlightTransactionDto>>> Handle(GetFlightTransactionQuery request, CancellationToken cancellationToken)
        {
            var userId = _getCurrentUser.UserId;

            var user = await _userRepository.GetByIdAsync(userId!.Value, cancellationToken);

            if (user!.Id != userId)
            {
                return Result.Failure<PaginatedResultDto<FlightTransactionDto>>(new Error("ResourceNotFound"));
            }

            (IList<FlightTransaction> flightTransactions, int totalCount) = await _flightTransactionRepository.GetAllByUserIdAsync(
                userId!.Value,
                request.LastSeenId,
                request.PageSize,
                cancellationToken);

            if (flightTransactions == null || flightTransactions.Count == 0)
            {
                return Result.Failure<PaginatedResultDto<FlightTransactionDto>>(new Error("FlightTransactionNotFound"));
            }

            var lastSeenId = flightTransactions.Any() ? flightTransactions.LastOrDefault()!.Id : request.LastSeenId;

            var paginatedFlightTransactionDtos = new PaginatedResultDto<FlightTransactionDto>
            {
                Items = flightTransactions.Select(ft => new FlightTransactionDto
                {
                    Id = ft.Id,
                    Status = ft.Status,
                    PaymentMethod = ft.PaymentMethod,
                    TotalCost = ft.TotalCost,
                    Discount = ft.Discount,
                    PaidAmount = ft.PaidAmount,
                    NumberOfSeatBooked = ft.NumberOfSeatBooked,
                    FlightSeatClassId = ft.FlightSeatClassId,
                }).ToList(),
                TotalCount = totalCount,
                LastSeenId = lastSeenId,
            };

            return Result.Success(paginatedFlightTransactionDtos);
        }
    }
}
