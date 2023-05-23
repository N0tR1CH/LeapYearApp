using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using LeapYearApp.Models.ViewModels;
using LeapYearApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace LeapYearApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddYearNameForm AddYearNameFormRequest { get; set; }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly IYearNameFormRepository _yearNameFormRepository;

        public IndexModel(ILogger<IndexModel> logger, IYearNameFormRepository yearNameFormRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _yearNameFormRepository = yearNameFormRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AddYearNameFormRequest.PublishedDate = DateTime.Now;

            bool isFemale = false;
            if (AddYearNameFormRequest.Name != null)
            {
                isFemale = AddYearNameFormRequest.Name.ToLower().EndsWith('a');
            }

            bool isUserSignedIn = _signInManager.IsSignedIn(User);

            Guid userId = Guid.Empty;
            string username = "Anonymous";

            if(isUserSignedIn)
            {
                userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                IdentityUser user = await _userManager.FindByIdAsync(userId.ToString());
                username = user.UserName;
            }

            var yearNameForm = new YearNameForm()
            {
                Year = AddYearNameFormRequest.Year,
                Name = AddYearNameFormRequest.Name,
                PublishedDate = AddYearNameFormRequest.PublishedDate,
                IsLeapYear = DateTime.IsLeapYear(AddYearNameFormRequest.Year),
                IsFemale = isFemale,
                UserId = userId,
                Login = username
            };


            await _yearNameFormRepository.AddAsync(yearNameForm);

            string message;

            if (yearNameForm.IsLeapYear)
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