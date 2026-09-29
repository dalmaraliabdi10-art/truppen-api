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
     private readonly IPlayerService _service;
     // Konstruktor för PlayersController som tar emot IPlayerService via dependency injection.
    public PlayersController(IPlayerService service) => _service = service;
    // Ger tillgång till metoderna definierade i IPlayerService för att hantera spelare.
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
    [HttpPut("({id:int})")]
    public async Task<ActionResult<PlayerReadDto>> Update(int id, [FromBody] PlayerUpdateDto dto)
    { // Uppdaterar en befintlig spelare baserat på ID och PlayerUpdateDto.
    // Om spelaren inte finns returneras 404 Not Found, annars returneras den uppdaterade spelarens information.
        var updated = await _service.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound(new { message = $"Ingen spelare med id {id} hittades." });

        return Ok(updated);
    }
}