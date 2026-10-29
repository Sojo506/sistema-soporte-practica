using Abstracciones.Agentes;
using Abstracciones.Comunes;
using Abstracciones.Excepciones;

namespace Flujo.Agentes
{
    public class AgenteFlujo : IAgenteFlujo
    {
        private readonly IAgenteDA _da;
        private readonly TimeProvider _reloj;

        public AgenteFlujo(IAgenteDA da, TimeProvider reloj)
        {
            _da = da;
            _reloj = reloj;
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
            var agente = await _da.ObtenerAsync(id, ct) ?? throw new NoEncontradoException("Agente", id);
            return AgenteMapeador.ADto(agente);
        }

        public async Task<AgenteDto> CrearAsync(CrearAgenteDto dto, CancellationToken ct)
        {
            var nuevoAgente = new Agente
            {
                Nombre = dto.Nombre.Trim(),
                Correo = dto.Correo.Trim().ToLowerInvariant(),
                Telefono = dto.Telefono.Trim(),
                CapacidadMaxima = dto.CapacidadMaxima,
                FechaRegistro = _reloj.GetUtcNow().UtcDateTime
            };
            var creado = await _da.CrearAsync(nuevoAgente, ct);
            return AgenteMapeador.ADto(creado);
        }

        public async Task ActualizarAsync(int id, ActualizarAgenteDto dto, CancellationToken ct)
        {
            var actualizado = await _da.ActualizarAsync(new Agente
            {
                Id = id,
                Nombre = dto.Nombre.Trim(),
                Correo = dto.Correo.Trim().ToLowerInvariant(),
                Telefono = dto.Telefono.Trim(),
                CapacidadMaxima = dto.CapacidadMaxima
            }, ct);

            if (!actualizado) throw new NoEncontradoException("Agente", id);
        }

        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            var eliminado = await _da.EliminarAsync(id, ct);
            if (!eliminado) throw new NoEncontradoException("Agente", id);
        }
    }
}
