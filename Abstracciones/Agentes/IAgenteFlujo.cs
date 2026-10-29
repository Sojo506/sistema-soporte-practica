using Abstracciones.Comunes;

namespace Abstracciones.Agentes
{
    public interface IAgenteFlujo
    {
        Task<ResultadoPaginado<AgenteDto>> ListarAsync(AgenteFiltro filtro, CancellationToken ct);
        Task<AgenteDto> ObtenerAsync(int id, CancellationToken ct);
        Task<AgenteDto> CrearAsync(CrearAgenteDto dto, CancellationToken ct);
        Task ActualizarAsync(int id, ActualizarAgenteDto dto, CancellationToken ct);
        Task EliminarAsync(int id, CancellationToken ct);

    }
}
