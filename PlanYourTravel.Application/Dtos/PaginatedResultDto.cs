namespace PlanYourTravel.Application.Dtos
{
    public class PaginatedResultDto<T>
    {
        public IList<T> Items { get; set; } = new List<T>();

        public int TotalCount { get; set; }

        public Guid LastSeenId { get; set; }
    }
}
