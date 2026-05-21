using System.ComponentModel.DataAnnotations;

namespace InterviewCopilot.Api.Dtos
{
    public class CreateJobApplicationDto
    {
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

        public decimal? ExpectedSalary { get; set; }

        public string? Notes { get; set; }
    }
}