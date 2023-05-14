namespace LeapYearApp.Models.ViewModels
{
    public class AddYearNameForm
    {
        public int Year { get; set; }
        public string? Name { get; set; }
        public string Result { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsFemale { get; set; }
    }
}
