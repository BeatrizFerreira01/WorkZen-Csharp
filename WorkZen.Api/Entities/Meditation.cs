using System.ComponentModel.DataAnnotations;

namespace WorkZen.Api.Entities;

public class Meditation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; }

    public int DurationMinutes { get; set; }

    public string Category { get; set; } = string.Empty;

    public bool IsPremium { get; set; }

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}