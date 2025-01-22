using System.ComponentModel.DataAnnotations;

namespace PlanYourTravel.WebApi.Models.Request
{
    public class GetFlightScheduleRequest
    {
        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public Guid DepartFrom { get; set; }

        [Required]
        public Guid ArriveAt { get; set; }

        public Guid LastSeenId { get; set; }

        [Required]
        public int PageSize { get; set; } = 25;
    }
}
