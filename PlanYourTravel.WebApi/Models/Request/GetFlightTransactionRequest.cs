namespace PlanYourTravel.WebApi.Models.Request
{
    public class GetFlightTransactionRequest
    {
        public Guid LastSeenId { get; set; }
        public int PageSize { get; set; } = 25;
    }
}
