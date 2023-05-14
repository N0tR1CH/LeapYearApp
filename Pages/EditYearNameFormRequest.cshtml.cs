using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeapYearApp.Pages
{
    public class EditYearNameFormRequestModel : PageModel
    {
        private readonly LeapYearAppDbContext _leapYearAppDbContext;

        [BindProperty]
        public YearNameForm YearNameForm { get; set; }

        public EditYearNameFormRequestModel(LeapYearAppDbContext leapYearAppDbContext)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
        }

        public void OnGet(Guid id)
        {
            YearNameForm = _leapYearAppDbContext.YearNameForms.Find(id);
        }

        public IActionResult OnPost()
        {
            var existingYearNameForm = _leapYearAppDbContext.YearNameForms.Find(YearNameForm.Id);            

            bool isFemale = false;
            if (YearNameForm.Name != null) 
            {
                isFemale = YearNameForm.Name.ToLower().EndsWith('a');
            }

            if (existingYearNameForm != null) 
            {
                existingYearNameForm.Name = YearNameForm.Name;
                existingYearNameForm.Year = YearNameForm.Year;
                existingYearNameForm.IsFemale = isFemale;
                existingYearNameForm.IsLeapYear = DateTime.IsLeapYear(YearNameForm.Year);
            }

            _leapYearAppDbContext.SaveChanges();

            return RedirectToPage("/SearchHistory");
        }
    }
}
