namespace PlanYourTravel.WebApi.Models.Request
{
    public class GetAirportRequest
    {
        public Guid LastSeenId { get; set; }
        public int PageSize { get; set; } = 25;
    }
}
