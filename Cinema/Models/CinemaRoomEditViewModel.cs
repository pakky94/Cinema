using CinemaClient.Domain;

namespace CinemaClient.Models;

public record CinemaRoomEditViewModel(CinemaRoom Room, IEnumerable<MovieScreening>? Screenings)
{
}
