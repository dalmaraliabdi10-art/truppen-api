using TruppenApi.Models;

namespace TruppenApi.Data;
// Seedningen körs bara när tabellen är tom. Annars hade man fått nya spelare varje gång appen startade.
// Seedningen sker i Program.cs efter att databasen har skapats. Detta säkerställer att seedningen endast sker när databasen är ny eller tom.
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Players.Any()) return;

        db.Players.AddRange(
            new Player { Namn = "Alisson Becker", Nummer = 1, Position = Position.Målvakt,
                Anteckning = "Trygg på linjen och bra på att styra backlinjen, är en exellent bollfördelare." },

            new Player { Namn = "Conor Bradley", Nummer = 12, Position = Position.Högerback,
                Anteckning = "En traditionell högerback med god teknik, gillar glidtackling." },

            new Player { Namn = "Kerkez", Nummer = 6, Position = Position.Vänsterback,
                Anteckning = "Snabb i återhämtningen, stabil defensivt samt offensivt." },

            new Player { Namn = "Virgel Van Dijk", Nummer = 4, Position = Position.Mittback,
                Anteckning = "Lugn i uppspelsfasen, stark i luftduellerna, dominerar i försvaret, elit CB." },

            new Player { Namn = "Jeremy Jacquet", Nummer = 5, Position = Position.Mittback,
                Anteckning = "En efterträdare av VVD och Matip" },

            new Player { Namn = "Ryan Gravenberch", Nummer =38, Position = Position.Defensivmittfältare,
                Anteckning = "Bryter passningsvägar och kylig vid bollen." },

            new Player { Namn = "Dominik Szoboszlai", Nummer = 8, Position = Position.Centralmittfältare,
                Anteckning = "Box to box, springer mest i laget." },

            new Player { Namn = "Florian Wirtz", Nummer = 10, Position = Position.Offensivmittfältare,
                Anteckning = "Kreativ i ytan mellan leden, en elit playmaker." },

            new Player { Namn = "Bradley Barcola", Nummer = 29, Position = Position.Högerytter,
                Anteckning = "Snabb, Elit one on one samt vid runs behind." },
            
            new Player { Namn = "Cody Gakpo", Nummer = 18, Position = Position.Vänsterytter,
                Status = PlayerStatus.Skadad,
                Anteckning = "Snabb, Elit cut in shoot, nu mer switchar mellan sidor och är oförsägbar spelare." },
            new Player { Namn = "Alexander Isak", Nummer = 9, Position = Position.Anfallare,
                Anteckning = "Stark i straffområdet, avslutar med båda fötterna." }
        );

        db.SaveChanges();
    }
}