namespace InterviewCopilot.Api.DTOs
{
    public class UpdateCompanyDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Industry { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
    }
}