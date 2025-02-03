namespace PlanYourTravel.Application.Dtos
{
    public class PaginatedResultDto<T>
    {
        public IList<T> Items { get; set; } = [];

        public int TotalCount { get; set; }

        public Guid LastSeenId { get; set; }
    }
}
