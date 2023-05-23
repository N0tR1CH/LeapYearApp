using LeapYearApp.Data;
using LeapYearApp.Models.Domain;
using LeapYearApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeapYearApp.Repositories
{
    public class YearNameFormRepository : IYearNameFormRepository
    {
        private LeapYearAppDbContext _leapYearAppDbContext;

        public YearNameFormRepository(LeapYearAppDbContext leapYearAppDbContext)
        {
            _leapYearAppDbContext = leapYearAppDbContext;
        }
        public async Task<YearNameForm> AddAsync(YearNameForm yearNameForm)
        {
            await _leapYearAppDbContext.YearNameForms.AddAsync(yearNameForm);
            await _leapYearAppDbContext.SaveChangesAsync();
            return yearNameForm;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingYearNameForm = await _leapYearAppDbContext.YearNameForms.FindAsync(id);

            if (existingYearNameForm != null)
            {
                _leapYearAppDbContext.Remove(existingYearNameForm);
                await _leapYearAppDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<YearNameForm>> GetAllAsync()
        {
            return await _leapYearAppDbContext.YearNameForms.ToListAsync(); 
        }

        public async Task<YearNameForm> GetByIdAsync(Guid id)
        {
            return await _leapYearAppDbContext.YearNameForms.FindAsync(id);
        }

        public async Task<YearNameForm> UpdateAsync(YearNameForm yearNameForm)
        {
            var existingYearNameForm = await _leapYearAppDbContext.YearNameForms.FindAsync(yearNameForm.Id);            

            bool isFemale = false;
            if (yearNameForm.Name != null) 
            {
                isFemale = yearNameForm.Name.ToLower().EndsWith('a');
            }

            if (existingYearNameForm != null) 
            {
                existingYearNameForm.Name = yearNameForm.Name;
                existingYearNameForm.Year = yearNameForm.Year;
                existingYearNameForm.IsFemale = isFemale;
                existingYearNameForm.IsLeapYear = DateTime.IsLeapYear(yearNameForm.Year);
            }

            await _leapYearAppDbContext.SaveChangesAsync();
            return existingYearNameForm;
        }
    }
}
