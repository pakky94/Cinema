namespace CinemaClient.Domain;

public class CinemaRoom
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
    public int? CurrentScreeningId { get; set; }
    public MovieScreening? CurrentScreening { get; set; }

    public void AddSpectator(Spectator spectator)
    {
        throw new NotImplementedException();
    }

    public void EmptyRoom()
    {
        throw new NotImplementedException();
    }
}
