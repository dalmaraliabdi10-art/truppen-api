using TruppenApi.Data;
using TruppenApi.Dtos;
using TruppenApi.Models;

namespace TruppenApi.Services;

public class PlayerService : IPlayerService
{ // Service implementation för att hantera spelare. 
// Den använder IPlayerRepository för att interagera med databasen
// och implementerar metoderna definierade i IPlayerService.
    private readonly IPlayerRepository _repository;

    public PlayerService(IPlayerRepository repository) => _repository = repository; // tar emot IplayerRepository via injektion
    // sedan används den för att hämta, skapa och uppdatera spelare i databasen.

    public async Task<IReadOnlyList<PlayerReadDto>> GetAllAsync()
    { // Hämtar alla spelare från databasen via repository och konverterar dem till PlayerReadDto innan de returneras.
        var players = await _repository.GetAllAsync();
        return players.Select(ToDto).ToList();
    }

    public async Task<PlayerReadDto?> GetByIdAsync(int id)
    { // Hämtar en spelare från databasen via repository baserat på ID och konverterar den till PlayerReadDto innan den returneras.
        var player = await _repository.GetByIdAsync(id);
        return player is null ? null : ToDto(player);
    }

    public async Task<PlayerReadDto> CreateAsync(PlayerCreateDto dto)
    { // Skapar en ny spelare baserat på PlayerCreateDto, lägger till den i databasen via repository och returnerar den skapade spelaren som PlayerReadDto.
        var player = new Player
        {
            Namn = dto.Namn.Trim(),
            Nummer = dto.Nummer,
            Anteckning = dto.Anteckning.Trim(),
            Position = dto.Position,
            Status = PlayerStatus.Tillgänglig,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(player);
        return ToDto(created);
    }

    public async Task<PlayerReadDto?> UpdateAsync(int id, PlayerUpdateDto dto)
    { // Uppdaterar en befintlig spelare baserat på PlayerUpdateDto, uppdaterar den i databasen via repository och returnerar den uppdaterade spelaren som PlayerReadDto.
        var player = await _repository.GetByIdAsync(id);
        if (player is null) return null;

        player.Namn = dto.Namn.Trim();
        player.Nummer = dto.Nummer;
        player.Anteckning = dto.Anteckning.Trim();
        player.Position = dto.Position;
        player.Status = dto.Status;
        // Man kan inte uppdatera CreatedAt eftersom det är när spelaren skapades, inte när den uppdaterades.

        await _repository.UpdateAsync(player);
        return ToDto(player);
    }

    public async Task<PlayerReadDto?> SetImageAsync(int id, string bildPath)
    { // Sätter bild för en spelare baserat på ID och bildens sökväg, uppdaterar den i databasen via repository och returnerar den uppdaterade spelaren som PlayerReadDto.
        var player = await _repository.GetByIdAsync(id);
        if (player is null) return null;

        player.BildPath = bildPath;
        await _repository.UpdateAsync(player);
        return ToDto(player);
    }

    private static PlayerReadDto ToDto(Player p) => new()
    { // Konverterar en Player till PlayerReadDto för att returnera relevant information till klienten.
        Id = p.Id,
        Namn = p.Namn,
        Nummer = p.Nummer,
        Anteckning = p.Anteckning,
        Position = p.Position,
        Linje = p.Position.GetLinje(),
        KlassisktNummer = p.Position.GetKlassisktNummer(),
        Status = p.Status,
        BildPath = p.BildPath,
        CreatedAt = p.CreatedAt
    };
}