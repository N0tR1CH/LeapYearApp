using LeapYearApp.Data;
using LeapYearApp.Extensions;
using LeapYearApp.Models.Domain;
using LeapYearApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LeapYearApp.Pages
{
    public class SearchHistoryModel : PageModel
    {
        private readonly LeapYearAppDbContext _leapYearAppDbContext;
        private readonly IConfiguration _configuration;
        private readonly IYearNameFormRepository _yearNameFormRepository;
        public PaginatedList<YearNameForm> YearNameForms { get; set; }

        public SearchHistoryModel(LeapYearAppDbContext leapYearAppDbContext, IConfiguration configuration, IYearNameFormRepository yearNameFormRepository)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
            _configuration = configuration;
            _yearNameFormRepository = yearNameFormRepository;
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
            var deleted = await _yearNameFormRepository.DeleteAsync(id);

            if (deleted)
            {
                return RedirectToPage("SearchHistory");
            }
            return Page();
        }
    }
}
