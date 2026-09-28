using System.ComponentModel.DataAnnotations;

namespace TruppenApi.Models;

public class Player
{ // Denna klass representerar en spelare i truppen och innehåller information om spelarens namn, nummer, position, status och andra relevanta attribut.
// innehåller även en property för att lagra sökvägen till spelarens bild och en timestamp för när spelaren skapades.
    public int Id { get; set; } //Primär nyckel för spelaren, unik id som identifierar spelarna i databasen.
    [Required]
    [MaxLength(100)]
    public string Namn { get; set; } = string.Empty; // Namn på spelaren samt max 100 tecekn

    public int Nummer { get; set; } // Tröjnummret för spelaren

    public string Anteckning { get; set; } = string.Empty; // "analys" / kommentar om spelaren

    public Position Position { get; set; } // Spelarens position

    public PlayerStatus Status { get; set; } = PlayerStatus.Tillgänglig; // Spelarens tillgänglighet

    // Relativ sökväg till bilden, t.ex. /uploads/abc123.jpg. Null tills en bild laddats upp.
    public string? BildPath { get; set; } // Null tills en bild laddas upp

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Datum och tid då spelaren skapades
}