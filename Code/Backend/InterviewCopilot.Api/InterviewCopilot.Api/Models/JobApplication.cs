using InterviewCopilot.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterviewCopilotAPI.Models
{
    public class JobApplication
    {
        [Key]
        public int JobApplicationId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(150)]
        public string JobTitle { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? JobDescriptionUrl { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApplicationStatus { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? ExpectedSalary { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public Company? Company { get; set; }
    }
}