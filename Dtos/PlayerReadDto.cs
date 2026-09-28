using TruppenApi.Models;

namespace TruppenApi.Dtos;
// DTO (Data Transfer Object) för att läsa spelarinformation
public class PlayerReadDto // Klienten får denna DTO när den hämtar spelarinformation från API:et. 
// Den innehåller alla relevanta fält som behövs för att visa information om en spelare.
{
    public int Id { get; set; }
    public string Namn { get; set; } = string.Empty; // Namn på spelaren samt max 100 tecken
    public int Nummer { get; set; }
    public string Anteckning { get; set; } = string.Empty; // "analys" / kommentar om spelaren
    public Position Position { get; set; }
    public Linje Linje { get; set; }
    public string? KlassisktNummer { get; set; } // Det klassiska numret för spelarens position.Vissa positioner saknar klassiskt nummer och returnerar null.
    public PlayerStatus Status { get; set; }
    public string? BildPath { get; set; } // Relativ sökväg till spelarens bild, t.ex. /uploads/abc123.jpg. Null tills en bild laddats upp.
    public DateTime CreatedAt { get; set; }
}