using System.ComponentModel.DataAnnotations;
using TruppenApi.Models;

namespace TruppenApi.Dtos;
// det klienten skickar till API, när de skapar. Den innehåller de fält som behövs för lägga till en ny spelare i databasen.
public class PlayerCreateDto
{
    [Required(ErrorMessage = "Namn måste anges.")]
    [MaxLength(100, ErrorMessage = "Namnet får vara högst 100 tecken.")]
    public string Namn { get; set; } = string.Empty; // Namn på spelaren samt max 100 tecken annars returneras ett felmeddelande till klienten.

    [Range(1, 99, ErrorMessage = "Tröjnumret måste vara mellan 1 och 99.")]
    public int Nummer { get; set; } // Tröjnummer på spelaren samt mellan 1 och 99 annars returneras ett felmeddelande till klienten.

    public string Anteckning { get; set; } = string.Empty;

    [Required]
    public Position Position { get; set; } // Spelarens position på planen. Valideras av enum Position. 
    // Om ett ogiltigt värde skickas returneras ett felmeddelande till klienten.
} // Fyra fält som medvetet exkluderats från DTO är: Id, Status, BildPath och CreatedAt. Dessa hanteras av servern och sätts automatiskt vid skapande av en ny spelare.