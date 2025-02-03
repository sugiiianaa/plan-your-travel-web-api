using PlanYourTravel.Domain.Common.Primitives;
using PlanYourTravel.Domain.Entities.UserAggregate;
using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Domain.Entities.Transactions
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

        public TransactionStatus Status { get; protected set; }
        public PaymentMethod PaymentMethod { get; protected set; }
        public DateTime PaymentDate { get; protected set; }
        public decimal TotalCost { get; protected set; }
        public decimal Discount { get; protected set; }
        public decimal PaidAmount { get; protected set; }

        // Foreign Keys
        public Guid UserId { get; protected set; }

        // Navigation Properties
        public User User { get; protected set; }
    }
}
