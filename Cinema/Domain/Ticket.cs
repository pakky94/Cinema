using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaClient.Domain;

public class Ticket
{
    public int Id { get; set; }
    [DisplayName("Spettatore")]
    public int SpectatorId { get; set; }
    public Spectator? Spectator { get; set; }
    [DisplayName("Film")]
    public int ScreeningId { get; set; }
    public MovieScreening? Screening { get; set; }
    [DisplayName("Posto")]
    public string Seat { get; set; } = string.Empty;
    [DisplayName("Prezzo")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
