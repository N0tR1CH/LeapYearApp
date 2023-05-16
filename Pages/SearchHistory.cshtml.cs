using LeapYearApp.Data;
using LeapYearApp.Extensions;
using LeapYearApp.Models.Domain;
using LeapYearApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace LeapYearApp.Pages
{
    public class SearchHistoryModel : PageModel
    {
        private readonly LeapYearAppDbContext _leapYearAppDbContext;
        private readonly IConfiguration _configuration;
        private readonly IYearNameFormRepository _yearNameFormRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PaginatedList<YearNameForm> YearNameForms { get; set; }

        public SearchHistoryModel(LeapYearAppDbContext leapYearAppDbContext, IConfiguration configuration, IYearNameFormRepository yearNameFormRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
            _configuration = configuration;
            _yearNameFormRepository = yearNameFormRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<YearNameForm> yearNameFormsIQ = from s in _leapYearAppDbContext.YearNameForms
                                                       select s;

            yearNameFormsIQ = yearNameFormsIQ.OrderByDescending(s => s.PublishedDate);

            int pageSize = _configuration.GetValue<int>("PageSize");
            YearNameForms = await PaginatedList<YearNameForm>.CreateAsync(
                               yearNameFormsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            var yearNameForm = await _yearNameFormRepository.GetByIdAsync(id);

            bool isUserSignedIn = _signInManager.IsSignedIn(User);

            if (!isUserSignedIn)
            {
                return RedirectToPage("SearchHistory");
            }
            else
            {
                Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                IdentityUser user = await _userManager.FindByIdAsync(userId.ToString());
                if (yearNameForm.UserId == userId && yearNameForm.Login == user.UserName)
                {
                    var deleted = await _yearNameFormRepository.DeleteAsync(id);

                    if (deleted)
                    {
                        return RedirectToPage("SearchHistory");
                    }
                }
            }
            return RedirectToPage("SearchHistory");
        }
    }
}
