using Microsoft.EntityFrameworkCore;
using TruppenApi.Models;

namespace TruppenApi.Data;

public class PlayerRepository : IPlayerRepository
{ // Repository-implementation för att hantera spelare i databasen.
// Den använder Entity Framework Core för att interagera med databasen och implementerar metoderna definierade i IPlayerRepository.
    private readonly AppDbContext _db; // DbContext som används för att interagera med databasen

    public PlayerRepository(AppDbContext db) => _db = db; // Konstruktor som tar emot en AppDbContext och tilldelar den till _db-fältet.
    // Anledningen till att man använder en DbContext är att den hanterar anslutningen till databasen,
    // spårar ändringar i entiteter och tillhandahåller metoder för att spara ändringar i databasen.

    public async Task<IReadOnlyList<Player>> GetAllAsync() => // Returnerar en lista med alla spelare i databasen
        await _db.Players 
            .AsNoTracking() // AsNoTracking används för att förbättra prestanda när man bara vill läsa data och inte göra några ändringar i den.
            .OrderBy(p => p.Nummer) // sortering efter spelarens nummer
            .ToListAsync(); // ToListAsync används för att asynkront hämta alla spelare från databasen och returnera dem som en lista.

    public async Task<Player?> GetByIdAsync(int id) => // Returnerar null om spelaren inte finns
        await _db.Players.FirstOrDefaultAsync(p => p.Id == id); // FirstOrDefaultAsync används för att asynkront hämta en spelare med det angivna id:t från databasen.
        // Om ingen spelare hittas returneras null.

    public async Task<Player> AddAsync(Player player)
    { // Lägger till en ny spelare i databasen och returnerar den skapade spelaren
        _db.Players.Add(player);
        await _db.SaveChangesAsync();
        return player;
    }

    public async Task<bool> UpdateAsync(Player player)
    { // Uppdaterar en befintlig spelare i databasen. Returnerar true om uppdateringen lyckades, annars false
        _db.Players.Update(player);
        return await _db.SaveChangesAsync() > 0;
    }
}