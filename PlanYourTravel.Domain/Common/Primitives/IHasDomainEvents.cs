using MediatR;

namespace PlanYourTravel.Domain.Common.Primitives
{
    public interface IHasDomainEvents
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
