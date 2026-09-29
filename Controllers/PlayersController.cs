using Microsoft.AspNetCore.Mvc;
using TruppenApi.Dtos;
using TruppenApi.Services;
// Using dependency injection för att injicera IPlayerService i PlayersController.
// Detta gör att PlayersController kan använda metoderna definierade i IPlayerService för att hantera spelare.
namespace TruppenApi.Controllers;

[ApiController] // Kort om ApiController är att, den läser och validerar inkommande data från klienten, om problem uppstår retuneras en 400 bad request.
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _service; // IPlayerService injiceras via konstruktorn och används för att hantera spelardata.
    private readonly IFileStorageService _fileStorage; // IFileStorageService injiceras via konstruktorn och används för att hantera filuppladdningar.
    // Konstruktorn tar emot IPlayerService och IFileStorageService som parametrar och tilldelar dem till privata fält. 
    // Detta möjliggör användning av dessa tjänster i controller-metoderna.
    public PlayersController(IPlayerService service, IFileStorageService fileStorage) 
    {
        _service = service;
        _fileStorage = fileStorage;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerReadDto>>> GetAll()
    {// Hämtar alla spelare från databasen via service och returnerar dem som en lista med PlayerReadDto. Om inga spelare finns returneras en tom lista.
        var players = await _service.GetAllAsync();
        return Ok(players);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerReadDto>> GetById(int id)
    { // Hämtar en spelare baserat på ID. Om spelaren inte finns returneras 404 Not Found, annars returneras spelarinformationen.
        var player = await _service.GetByIdAsync(id);
        if (player is null)
            return NotFound(new { message = $"Ingen spelare med id {id} hittades." });
        return Ok(player);
    }
    [HttpPost]
    public async Task<ActionResult<PlayerReadDto>> Create([FromBody] PlayerCreateDto dto)
    { // Skapar en ny spelare baserat på PlayerCreateDto. Om skapandet lyckas returneras 201 Created med den skapade spelarens information.
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PlayerReadDto>> Update(int id, [FromBody] PlayerUpdateDto dto)
    { // Uppdaterar en befintlig spelare baserat på ID och PlayerUpdateDto.
    // Om spelaren inte finns returneras 404 Not Found, annars returneras den uppdaterade spelarens information.
        var updated = await _service.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound(new { message = $"Ingen spelare med id {id} hittades." });

        return Ok(updated);
    }
    [HttpPost("{id:int}/upload")]
    public async Task<ActionResult<PlayerReadDto>> Upload(int id, IFormFile file)
    {// Hanterar filuppladdning för en spelare baserat på ID. Om ingen fil bifogas returneras 400 Bad Request, om spelaren inte finns returneras 404 Not Found,
    // och om filuppladdningen misslyckas returneras 400 Bad Request med ett felmeddelande. Vid lyckad uppladdning returneras den uppdaterade spelarens information.
        if (file is null)
            return BadRequest(new { message = "Ingen fil bifogades." });

        var player = await _service.GetByIdAsync(id);
        if (player is null)
            return NotFound(new { message = $"Ingen spelare med id {id} hittades." });

        var result = await _fileStorage.SaveImageAsync(file);
        if (!result.Success)
            return BadRequest(new { message = result.Error });

        var updated = await _service.SetImageAsync(id, result.RelativePath!);
        return Ok(updated);
    }
}