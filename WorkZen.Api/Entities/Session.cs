using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkZen.Api.Entities;

public class Session
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Meditation")]
    public Guid MeditationId { get; set; }
    public Meditation? Meditation { get; set; }

    public DateTime StartedAt { get; set; }

    public int? Rating { get; set; }
    public string? Mood { get; set; }
}