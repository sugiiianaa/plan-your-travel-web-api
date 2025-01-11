using System.ComponentModel.DataAnnotations;

namespace PlanYourTravel.WebApi.Models
{
    public class CreateFlightScheduleRequest
    {
        [Required]
        [MaxLength(20)]
        public required IList<CreateFlightScheduleModel> FlightSchedules { get; set; }
    }

    public class CreateFlightScheduleModel
    {
        public required string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public Guid AirlineId { get; set; }
    }

}
