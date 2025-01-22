using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Application.Dtos
{
    public class FlightTransactionDto
    {
        public Guid Id { get; set; }
        public TransactionStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public int NumberOfSeatBooked { get; set; }
        public Guid FlightSeatClassId { get; set; }
    }
}
