using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using LeapYearApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeapYearApp.Pages
{
    public class EditYearNameFormRequestModel : PageModel
    {
        private readonly LeapYearAppDbContext _leapYearAppDbContext;
        private readonly IYearNameFormRepository _yearNameFormRepository;
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public YearNameForm YearNameForm { get; set; }

        public EditYearNameFormRequestModel(LeapYearAppDbContext leapYearAppDbContext, IYearNameFormRepository yearNameFormRepository, SignInManager<IdentityUser> signInManager)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
            _yearNameFormRepository = yearNameFormRepository;
            _signInManager = signInManager;
        }

        public async Task OnGet(Guid id)
        {
           YearNameForm = await _yearNameFormRepository.GetByIdAsync(id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            // Powrót blokuje edycje dla kazdego
            return RedirectToPage("/SearchHistory");
            await _yearNameFormRepository.UpdateAsync(YearNameForm);
        }
    }
}
