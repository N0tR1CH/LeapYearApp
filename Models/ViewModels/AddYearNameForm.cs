using System.ComponentModel.DataAnnotations;

namespace LeapYearApp.Models.ViewModels
{
    public class AddYearNameForm
    {
        [Required]
        [Range(1899,2022)]
        public int Year { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsFemale { get; set; }
    }
}
