using Abstracciones.Agentes;
using Abstracciones.Comunes;

namespace Flujo.Agentes
{
    public class AgenteFlujo : IAgenteFlujo
    {
        private readonly IAgenteDA _da;
        public AgenteFlujo(IAgenteDA da)
        {
            _da = da;
        }
        public async Task<ResultadoPaginado<AgenteDto>> ListarAsync(AgenteFiltro filtro, CancellationToken ct)
        {
            var resultado = await _da.ObtenerActivosPaginadoAsync(filtro, ct);
            return new ResultadoPaginado<AgenteDto> 
            {
                Items = resultado.Items.Select(AgenteMapeador.ADto).ToList(),
                Total = resultado.Total,
                Pagina = resultado.Pagina,
                Tamano = resultado.Tamano
            };
        }
        public async Task<AgenteDto> ObtenerAsync(int id, CancellationToken ct)
        {
            var agente = await _da.ObtenerAsync(id, ct);
            if (agente is null) throw new KeyNotFoundException($"No se encontró el agente con Id {id}.");
            return AgenteMapeador.ADto(agente);
        }

        public async Task<AgenteDto> CrearAsync(CrearAgenteDto dto, CancellationToken ct)
        {
            var nuevoAgente = new Agente
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                CapacidadMaxima = dto.CapacidadMaxima
            };
            var creado = await _da.CrearAsync(nuevoAgente, ct);
            return new AgenteDto(
                creado.Id,
                creado.Nombre,
                creado.Correo,
                creado.Telefono,
                creado.CapacidadMaxima,
                creado.Activo,
                creado.FechaRegistro);
        }
        public async Task ActualizarAsync(int id, ActualizarAgenteDto dto, CancellationToken ct)
        {
            var agenteExistente = await _da.ObtenerAsync(id, ct);
            if (agenteExistente is null) throw new KeyNotFoundException($"No se encontró el agente con Id {id}.");
            agenteExistente.Nombre = dto.Nombre;
            agenteExistente.Correo = dto.Correo;
            agenteExistente.Telefono = dto.Telefono;
            agenteExistente.CapacidadMaxima = dto.CapacidadMaxima;
            await _da.ActualizarAsync(agenteExistente, ct);
        }
        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            await _da.EliminarAsync(id, ct);
        }
    }
}
