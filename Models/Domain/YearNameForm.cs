namespace LeapYearApp.Models.Domain
{
    public class YearNameForm
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string? Login { get; set; }
        public string? Name { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsLeapYear { get; set; }
        public bool IsFemale { get; set; }
        public Guid UserId { get; set; }
    }
}
