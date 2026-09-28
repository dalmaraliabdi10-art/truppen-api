using System.ComponentModel.DataAnnotations;
using TruppenApi.Models;

namespace TruppenApi.Dtos;

public class PlayerUpdateDto
{
    [Required(ErrorMessage = "Namn måste anges.")]
    [MaxLength(100, ErrorMessage = "Namnet får vara högst 100 tecken.")]
    public string Namn { get; set; } = string.Empty;

    [Range(1, 99, ErrorMessage = "Tröjnumret måste vara mellan 1 och 99.")]
    public int Nummer { get; set; }

    public string Anteckning { get; set; } = string.Empty;

    [Required]
    public Position Position { get; set; }

    [Required]
    public PlayerStatus Status { get; set; }
} // likt PlayerCreateDto, men med ett extra fält för att uppdatera spelarens status.