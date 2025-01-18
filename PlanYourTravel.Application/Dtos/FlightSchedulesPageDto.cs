using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;

namespace PlanYourTravel.Application.Dtos
{
    public class FlightSchedulesPageDto
    {
        public IList<FlightSchedule> Schedules { get; set; } = [];
        public int TotalCount { get; set; }
        public Guid NextLastSeenId { get; set; }
    }
}
