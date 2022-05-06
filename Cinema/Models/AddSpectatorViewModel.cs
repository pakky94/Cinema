using CinemaClient.Domain;

namespace CinemaClient.Models
{
    public record AddSpectatorViewModel(MovieScreening Screening, IEnumerable<Spectator> Spectators, int? SpectatorId)
    {
    }
}
