using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CinemaClient.Domain;

public class MovieScreening
{
    public int Id { get; set; }
    [DisplayName("Film")]
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
    public int RoomId { get; set; }
    public CinemaRoom? Room { get; set; }
    [DisplayName("Orario")]
    public DateTime Time { get; set; }
    [DisplayName("Prezzo")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }

    [JsonIgnore]
    public bool IsFull { get => Room?.Capacity <= Tickets?.Count; }
    [JsonIgnore]
    public int EmptySeats { get => Room?.Capacity - Tickets?.Count ?? 0; }
}
