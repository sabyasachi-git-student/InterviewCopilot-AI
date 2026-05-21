namespace InterviewCopilot.Api.Dtos
{
    public class JobApplicationDto
    {
        public int JobApplicationId { get; set; }
        public int CompanyId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string? JobDescriptionUrl { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; } = string.Empty;
        public decimal? ExpectedSalary { get; set; }
        public string? Notes { get; set; }

        // Useful for display purposes in React
        public string? CompanyName { get; set; }
    }
}