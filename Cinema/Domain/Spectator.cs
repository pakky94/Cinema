namespace CinemaClient.Domain;

public class Spectator
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}
