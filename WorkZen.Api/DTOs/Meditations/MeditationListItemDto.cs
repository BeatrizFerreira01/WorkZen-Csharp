namespace WorkZen.Api.DTOs.Meditations;

public class MeditationListItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public bool IsPremium { get; set; }
}