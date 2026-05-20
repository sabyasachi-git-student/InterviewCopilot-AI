namespace InterviewCopilot.Api.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public int UserId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string? Website { get; set; }

        public string? Industry { get; set; }

        public string? Location { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}