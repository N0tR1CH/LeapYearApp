using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using LeapYearApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeapYearApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddYearNameForm AddYearNameFormRequest { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly LeapYearAppDbContext _leapYearAppDbContext;

        public IndexModel(ILogger<IndexModel> logger, LeapYearAppDbContext leapYearAppDbContext)
        {
            _logger = logger;
            _leapYearAppDbContext = leapYearAppDbContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // _addYearNameFormRequestService.AddYearNameForm(AddYearNameFormRequest);
            AddYearNameFormRequest.PublishedDate = DateTime.Now;

            bool isFemale = false;
            if (AddYearNameFormRequest.Name != null) 
            {
                isFemale = AddYearNameFormRequest.Name.ToLower().EndsWith('a');
            }

            var yearNameForm = new YearNameForm()
            {
                Year = AddYearNameFormRequest.Year,
                Name = AddYearNameFormRequest.Name,
                PublishedDate = AddYearNameFormRequest.PublishedDate,
                IsLeapYear = DateTime.IsLeapYear(AddYearNameFormRequest.Year),
                IsFemale = isFemale
            };

            await _leapYearAppDbContext.YearNameForms.AddAsync(yearNameForm);
            await _leapYearAppDbContext.SaveChangesAsync();

            return Page();
        }
    }
}