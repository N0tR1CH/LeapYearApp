using LeapYearApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LeapYearApp.Data
{
    public class LeapYearAppDbContext : DbContext
    {
        public LeapYearAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<YearNameForm> YearNameForms { get; set; }
    }
}
