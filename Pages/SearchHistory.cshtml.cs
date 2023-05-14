using LeapYearApp.Data;
using LeapYearApp.Extensions;
using LeapYearApp.Models.Domain;
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
        public PaginatedList<YearNameForm> YearNameForms { get; set; }

        public SearchHistoryModel(LeapYearAppDbContext leapYearAppDbContext, IConfiguration configuration)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
            _configuration = configuration;
        }

        public async Task OnGetAsync(string sortOder, string currentFilter, string searchString, int? pageIndex)
        {
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            IQueryable<YearNameForm> yearNameFormsIQ = from s in _leapYearAppDbContext.YearNameForms
                                                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                yearNameFormsIQ = yearNameFormsIQ.Where(s => s.Name.Contains(searchString)
                                                      || s.Year.ToString().Contains(searchString));
            }

            yearNameFormsIQ = yearNameFormsIQ.OrderByDescending(s => s.PublishedDate);

            int pageSize = _configuration.GetValue<int>("PageSize");
            YearNameForms = await PaginatedList<YearNameForm>.CreateAsync(
                               yearNameFormsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
