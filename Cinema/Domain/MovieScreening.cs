namespace CinemaClient.Domain;

public class MovieScreening
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
    public int RoomId { get; set; }
    public CinemaRoom? Room { get; set; }
    public DateTime Time { get; set; }
}
