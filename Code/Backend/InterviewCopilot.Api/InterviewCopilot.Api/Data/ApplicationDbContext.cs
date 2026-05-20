using InterviewCopilot.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewCopilot.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}