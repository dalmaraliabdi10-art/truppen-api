using TruppenApi.Models;

namespace TruppenApi.Data;
// repository interface för att hantera spelare i databasen. Den definierar metoder för att hämta, lägga till och uppdatera spelare. Den vet inte hur spelarna lagras,
// det är upp till implementeringen av interfacet att bestämma.
// Har inte deleteAsync eftersom vi inte vill ta bort spelare från databasen, utan istället markera dem som inaktiva med hjälp av PlayerStatus.
public interface IPlayerRepository
{
    Task<IReadOnlyList<Player>> GetAllAsync(); // Returnerar en lista med alla spelare i databasen
    Task<Player?> GetByIdAsync(int id); // Returnerar null om spelaren inte finns
    Task<Player> AddAsync(Player player); // Lägger till en ny spelare i databasen och returnerar den skapade spelaren
    Task<bool> UpdateAsync(Player player); // Uppdaterar en befintlig spelare i databasen. Returnerar true om uppdateringen lyckades, annars false
}