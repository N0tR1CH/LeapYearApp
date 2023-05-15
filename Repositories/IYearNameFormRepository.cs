using LeapYearApp.Models.Domain;

namespace LeapYearApp.Repositories
{
    public interface IYearNameFormRepository
    {
        Task<IEnumerable<YearNameForm>> GetAllAsync();
        Task<YearNameForm> GetByIdAsync(Guid id);
        Task<YearNameForm> AddAsync(YearNameForm yearNameForm);
        Task<YearNameForm> UpdateAsync(YearNameForm yearNameForm);
        Task<bool> DeleteAsync(Guid id);
    }
}
