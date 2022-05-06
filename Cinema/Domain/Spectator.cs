using System.ComponentModel;

namespace CinemaClient.Domain;

public class Spectator
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    [DisplayName("Data di nascita")]
    public DateTime BirthDate { get; set; }

    public string FullName() => $"{Name} {Surname}";

    public int Age(DateTime now)
    {
        return now.Year - BirthDate.Year;
    }

    public decimal PriceMultiplier(DateTime now)
    {
        var age = Age(now);
        if (age > 70)
            return 0.9m;

        if (age < 5)
            return 0.5m;

        return 1m;
    }
}
