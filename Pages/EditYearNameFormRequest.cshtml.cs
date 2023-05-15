using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using LeapYearApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeapYearApp.Pages
{
    public class EditYearNameFormRequestModel : PageModel
    {
        private readonly LeapYearAppDbContext _leapYearAppDbContext;
        private readonly IYearNameFormRepository _yearNameFormRepository;

        [BindProperty]
        public YearNameForm YearNameForm { get; set; }

        public EditYearNameFormRequestModel(LeapYearAppDbContext leapYearAppDbContext, IYearNameFormRepository yearNameFormRepository)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
            _yearNameFormRepository= yearNameFormRepository;
        }

        public async Task OnGet(Guid id)
        {
           YearNameForm = await _yearNameFormRepository.GetByIdAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            await _yearNameFormRepository.UpdateAsync(YearNameForm);
            return RedirectToPage("/SearchHistory");
        }
    }
}
