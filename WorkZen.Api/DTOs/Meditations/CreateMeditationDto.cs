namespace WorkZen.Api.DTOs.Meditations;

public class CreateMeditationDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsPremium { get; set; }
}