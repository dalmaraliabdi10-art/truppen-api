using TruppenApi.Dtos;

namespace TruppenApi.Services;

public interface IPlayerService
{ // Service interface för att hantera spelare. Den definierar metoder för att hämta, skapa och uppdatera spelare. Den vet inte hur spelarna lagras eller hanteras,
// det är upp till implementeringen av interfacet att bestämma.
    Task<IReadOnlyList<PlayerReadDto>> GetAllAsync(); //Hämtar info från PlayerReadDto.
    Task<PlayerReadDto?> GetByIdAsync(int id); //Hämtar info från PlayerReadDto baserat på ID.
    Task<PlayerReadDto> CreateAsync(PlayerCreateDto dto); //Skapar en ny spelare.
    Task<PlayerReadDto?> UpdateAsync(int id, PlayerUpdateDto dto); //Uppdaterar en befintlig spelare.
    Task<PlayerReadDto?> SetImageAsync(int id, string bildPath); //Sätter bild för en spelare.
}