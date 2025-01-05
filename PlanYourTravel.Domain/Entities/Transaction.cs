using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public abstract class Transaction : AuditableEntity
    {
        protected Transaction() : base() { }

        protected Transaction(
            Guid id,
            Guid userId,
            TransactionStatus status,
            PaymentMethod paymentMethod,
            DateTime paymentDate,
            decimal totalCost,
            decimal discount,
            decimal paidAmount)
            : base(id)
        {
            UserId = userId;
            Status = status;
            PaymentMethod = paymentMethod;
            PaymentDate = paymentDate;
            TotalCost = totalCost;
            Discount = discount;
            PaidAmount = paidAmount;
        }

        public Guid UserId { get; protected set; }
        public TransactionStatus Status { get; protected set; }
        public PaymentMethod PaymentMethod { get; protected set; }
        public DateTime PaymentDate { get; protected set; }
        public decimal TotalCost { get; protected set; }
        public decimal Discount { get; protected set; }
        public decimal PaidAmount { get; protected set; }

    }
}
