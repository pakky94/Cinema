using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaClient.Domain;

public class Ticket
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public CinemaRoom? Room { get; set; }
    public string Seat { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
