namespace WorkZen.Api.DTOs.Meditations;

public class SessionDto
{
    public Guid Id { get; set; }
    public DateTime StartedAt { get; set; }
    public int? Rating { get; set; }
    public string? Mood { get; set; }
}