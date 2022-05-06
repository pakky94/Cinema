using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaClient.Domain;

public class Ticket
{
    public int Id { get; set; }
    public int SpectatorId { get; set; }
    public Spectator? Spectator { get; set; }
    public int ScreeningId { get; set; }
    public MovieScreening? Screening { get; set; }
    public string Seat { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
