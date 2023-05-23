using LeapYearApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LeapYearApp.Data
{
    public class LeapYearAppDbContext : DbContext
    {
        public LeapYearAppDbContext(DbContextOptions<LeapYearAppDbContext> options) : base(options)
        {
        }

        public DbSet<YearNameForm> YearNameForms { get; set; }
    }
}
