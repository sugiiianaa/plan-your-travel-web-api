namespace PlanYourTravel.WebApi.Models.Request
{
    public class CreateFlightTransactionRequest
    {
        public int PaymentMethod { get; set; }
        public int NumberOfSeatBooked { get; set; }
        public Guid FlightSeatClassId { get; set; }
    }
}
