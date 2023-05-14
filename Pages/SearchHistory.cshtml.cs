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
            ViewData["CurrentSort"] = sortOder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOder) ? "name_desc" : "";
            ViewData["YearSortParm"] = sortOder == "Year" ? "year_desc" : "Year";
            ViewData["DateSortParm"] = sortOder == "Date" ? "date_desc" : "Date";
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
            switch (sortOder)
            {
                case "name_desc":
                    yearNameFormsIQ = yearNameFormsIQ.OrderByDescending(s => s.Name);
                    break;
                case "Year":
                    yearNameFormsIQ = yearNameFormsIQ.OrderBy(s => s.Year);
                    break;
                case "year_desc":
                    yearNameFormsIQ = yearNameFormsIQ.OrderByDescending(s => s.Year);
                    break;
                case "Date":
                    yearNameFormsIQ = yearNameFormsIQ.OrderBy(s => s.PublishedDate);
                    break;
                case "date_desc":
                    yearNameFormsIQ = yearNameFormsIQ.OrderByDescending(s => s.PublishedDate);
                    break;
                default:
                    yearNameFormsIQ = yearNameFormsIQ.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = _configuration.GetValue<int>("PageSize");
            YearNameForms = await PaginatedList<YearNameForm>.CreateAsync(
                               yearNameFormsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
