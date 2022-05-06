using System.ComponentModel;

namespace CinemaClient.Domain;

public class Movie
{
    public int Id { get; set; }
    [DisplayName("Titolo")]
    public string Title { get; set; } = string.Empty;
    [DisplayName("Autore")]
    public string Author { get; set; } = string.Empty;
    [DisplayName("Produttore")]
    public string Producer { get; set; } = string.Empty;
    [DisplayName("Genere")]
    public string Genre { get; set; } = string.Empty;
    [DisplayName("Durata")]
    public int Duration { get; set; }
}
