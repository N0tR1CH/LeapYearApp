using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using LeapYearApp.Models.ViewModels;
using LeapYearApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeapYearApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddYearNameForm AddYearNameFormRequest { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IYearNameFormRepository _yearNameFormRepository;

        public IndexModel(ILogger<IndexModel> logger, IYearNameFormRepository yearNameFormRepository)
        {
            _logger = logger;
            _yearNameFormRepository = yearNameFormRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
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

            await _yearNameFormRepository.AddAsync(yearNameForm);

            string message;

            if(yearNameForm.IsLeapYear)
            {
                message = "To był rok przestępny.";
            }
            else
            {
                message = "To nie był rok przestępny.";
            }

            string verb;

            if (yearNameForm.Name == null)
            {
                verb = "";
            } 
            else
            {
                if (isFemale)
                {
                    verb = "urodziła";
                }
                else
                {
                    verb = "urodził";
                }
                message = $"{yearNameForm.Name} {verb} się w {yearNameForm.Year} roku. {message}";
            }


            ViewData["MessageDescription"] = message;

            return Page();
        }
    }
}