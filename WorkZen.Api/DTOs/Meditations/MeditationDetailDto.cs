namespace WorkZen.Api.DTOs.Meditations;

public class MeditationDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsPremium { get; set; }
    public List<SessionDto> Sessions { get; set; } = new();
}