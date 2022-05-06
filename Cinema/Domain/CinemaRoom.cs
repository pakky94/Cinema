using System.ComponentModel;

namespace CinemaClient.Domain;

public class CinemaRoom
{
    public int Id { get; set; }
    [DisplayName("Capacità")]
    public int Capacity { get; set; }
    [DisplayName("Proiezione")]
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
