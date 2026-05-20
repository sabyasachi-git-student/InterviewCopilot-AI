namespace InterviewCopilot.Api.DTOs
{
    public class CreateCompanyDto
    {
        public int UserId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string? Website { get; set; }

        public string? Industry { get; set; }

        public string? Location { get; set; }

        public string? Notes { get; set; }
    }
}