using Abstracciones.Agentes;
using Abstracciones.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentesControllers : ControllerBase
{
    private readonly IAgenteFlujo _flujo;
    public AgentesControllers(IAgenteFlujo flujo) => _flujo = flujo;


    [HttpGet]
    [ProducesResponseType<IReadOnlyList<AgenteDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Obtener(int id, CancellationToken ct) =>
        Ok(await _flujo.ObtenerAsync(id, ct));

    [HttpGet]
    [ProducesResponseType<IReadOnlyList<AgenteDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Listar([FromQuery] AgenteFiltro filtro)
    {
        var resultado = await _flujo.ListarAsync(filtro, CancellationToken.None);
        Response.Headers["X-Total-Count"] = resultado.Total.ToString();
        Response.Headers["X-Page"] = resultado.Pagina.ToString();
        Response.Headers["X-Page-Size"] = resultado.Tamano.ToString();
        return Ok(resultado.Items);
    }

    [HttpPost]
    [ProducesResponseType<ClienteDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Crear(CrearAgenteDto dto)
    {
        var creado = await _flujo.CrearAsync(dto, CancellationToken.None);
        return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Actualizar(int id, ActualizarAgenteDto dto, CancellationToken ct)
    {
        await _flujo.ActualizarAsync(id, dto, ct);
        return NoContent();
    }

    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
    {
        await _flujo.EliminarAsync(id, ct);
        return NoContent();
    }
}
