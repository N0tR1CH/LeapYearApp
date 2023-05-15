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

        public async Task OnGet(Guid id)
        {
           YearNameForm = await _leapYearAppDbContext.YearNameForms.FindAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            var existingYearNameForm = await _leapYearAppDbContext.YearNameForms.FindAsync(YearNameForm.Id);            

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

            await _leapYearAppDbContext.SaveChangesAsync();

            return RedirectToPage("/SearchHistory");
        }
    }
}
