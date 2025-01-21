namespace PlanYourTravel.WebApi.Models.Request
{
    public class GetFlightScheduleRequest
    {
        public DateTime DepartureDate { get; set; }
        public Guid DepartFrom { get; set; }
        public Guid ArriveAt { get; set; }
        public Guid LastSeenId { get; set; }
        public int PageSize { get; set; }
    }
}
