using System.ComponentModel.DataAnnotations;

namespace PlanYourTravel.WebApi.Models
{
    public class CreateFlightSeatClassRequest
    {
        [Required]
        public Guid FlightScheduleId { get; set; }

        [Required]
        [MaxLength(20)]
        public required IList<CreateFlightSeatClassModel> FlightSeatClassModels { get; set; }

        public class CreateFlightSeatClassModel
        {
            public required int SeatClassType { get; set; }
            public int? SeatBooked { get; set; }
            public required int Capacity { get; set; }
            public required int Price { get; set; }
        }
    }
}
