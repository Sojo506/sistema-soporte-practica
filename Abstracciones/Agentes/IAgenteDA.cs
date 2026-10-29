using Abstracciones.Comunes;

namespace Abstracciones.Agentes
{
    public interface IAgenteDA
    {
        Task<Agente?> ObtenerAsync(int id, CancellationToken ct);
        // get para agentes activos y paginados
        Task<ResultadoPaginado<Agente>> ObtenerActivosPaginadoAsync(AgenteFiltro filtro, CancellationToken ct);
        // crear agente
        Task<Agente> CrearAsync(Agente agente, CancellationToken ct);
        // actualizar agente
        Task<bool> ActualizarAsync(Agente agente, CancellationToken ct);
        // eliminar agente
        Task<bool> EliminarAsync(int id, CancellationToken ct);

    }
}
