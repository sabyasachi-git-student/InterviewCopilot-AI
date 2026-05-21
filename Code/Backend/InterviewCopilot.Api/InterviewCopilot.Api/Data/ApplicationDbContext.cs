using InterviewCopilot.Api.Models;
using InterviewCopilotAPI.Models;
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

        public DbSet<JobApplication> JobApplications { get; set; }
    }
}