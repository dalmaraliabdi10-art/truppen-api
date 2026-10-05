Truppen API

REST-API för spelarregistret Truppen. Hanterar datalagring, bilduppladdning och serverar samma data till både webbappen och mobilappen.

--

Teknik

ASP.NET Core WebAPI, 

.NET 10

Entity Framework Core 10 med SQLite

Swashbuckle för Swagger

Tre lager: Controller, Service, Repository

--

Köra projektet

Kräver .NET 10 SDK.

git clone <repo-url>

cd truppen-api

dotnet build

dotnet run

API startar på http://localhost:5275

Swagger finns på http://localhost:5275/swagger

Databasen truppen.db skapas automatiskt vid första starten och fylls med elva spelare. Uppladdade bilder hamnar i wwwroot/uploads.

--

Endpoints

GET /api/players – alla spelare, sorterade på tröjnummer

GET /api/players/{id} – en spelare, 404 om den saknas

POST /api/players – skapar en spelare, returnerar 201

PUT /api/players/{id} – uppdaterar uppgifter och status

POST /api/players/{id}/upload – laddar upp en bild till en spelare

Bilder serveras som statiska filer via /uploads/filnamn

Färdiga testanrop finns i truppen-api.http, inklusive två negativa tester som visar att ogiltig position ger 400 och att en spelare som inte finns ger 404.

--

Struktur

Controllers – HTTP-lagret: routing och statuskoder (Http)

Services – logik och mappning till DTO (Affärslogik)

Data – repository, DbContext och seed-data (info)

Dtos – in- och utdata mot klienterna(kontrakt)

Models – entiteter och enums (mall)

--

CORS

Konfigurerad i Program.cs.

I utvecklingsläge tillåts alla origins, eftersom Vite kan byta port och telefonen når API via en IP som ändras mellan nätverk. I produktion läses en låst lista från Cors:AllowedOrigins i appsettings.json.

CORS är en mekanism i webbläsaren och gäller bara webbappen. Mobilappen skickar ingen Origin-header och berörs inte.

--

Val jag gjort

Tre lager. Controllern hanterar bara HTTP. Logiken ligger i servicen och databasåtkomsten bakom IPlayerRepository. Det gör att jag kan byta datalagring utan att röra controllern.

Separata DTO. Klienten ser aldrig Player direkt. PlayerCreateDto saknar Id, Status, BildPath och CreatedAt – de fälten äger servern och utan DTO skulle en klient kunna sätta dem själv.

Validering med DataAnnotations. Required, Range 1 till 99 och MaxLength 100 ligger på DTO. ApiController kör dem automatiskt och svarar 400 innan kod anropas. Filvalideringen, alltså filtyp och storleksgräns, ligger i FileStorageService.

Linje och klassiskt nummer räknas ut, lagras inte. De förklarar ur positionen i PlayerService när DTO byggs. Sparade jag dem i databasen skulle de kunna hamna i otakt med positionen.

GUID som filnamn. Två uppladdningar med samma namn krockar inte och ett filnamn från klienten kan inte användas för att skriva utanför uploads-mappen.

IFileStorageService som interface. Lagringen kan bytas mot molnlagring utan att servicen ändras.

--

Förbättringar för framtiden, nuläget:

Ingen DELETE-endpoint

Ingen inloggning

Två spelare kan ha samma tröjnummer – Range kollar intervallet, inte om numret är upptaget
