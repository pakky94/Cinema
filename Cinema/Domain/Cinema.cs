namespace CinemaClient.Domain;

public class Cinema
{
    public int Id { get; set; }
    public List<CinemaRoom> Rooms { get; set; } = null!;
}
