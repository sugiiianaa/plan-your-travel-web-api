namespace PlanYourTravel.WebApi.Models
{
    public class CreateFlightSeatClassRequest
    {
        public Guid FlightScheduleId { get; set; }
        public int SeatClassType { get; set; }
        public int Capacity { get; set; }
        public int Price { get; set; }
    }
}
