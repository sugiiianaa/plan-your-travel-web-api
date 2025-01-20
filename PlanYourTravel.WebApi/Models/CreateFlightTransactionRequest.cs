namespace PlanYourTravel.WebApi.Models
{
    public class CreateFlightTransactionRequest
    {
        public int PaymentMethod { get; set; }
        public int NumberOfSeatBooked { get; set; }
        public Guid FlightSeatClassId { get; set; }
    }
}
