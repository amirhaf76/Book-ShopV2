namespace Infrastructure.UsefulDtos
{
    public class TimeFilterDto
    {
        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
    }
}
