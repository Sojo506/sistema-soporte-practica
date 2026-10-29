using Abstracciones.Agentes;
using Abstracciones.Clientes;
using Abstracciones.Comunes;
using Abstracciones.Excepciones;
using DA.Contexto;
using Microsoft.EntityFrameworkCore;

// completa todas las operaciones de la interfaz IAgenteDA, incluyendo la paginación de agentes activos y la creación de nuevos agentes.

namespace DA.Agentes
{
    public class AgenteDA : IAgenteDA
    {
        private readonly SistemaSoporteContexto _ctx;

        public AgenteDA(SistemaSoporteContexto ctx) => _ctx = ctx;

        public Task<Agente> ObtenerAsync(int id, CancellationToken ct)
        {
            // AsNoTracking: solo leemos, EF no necesita vigilar los objetos.
            return _ctx.Agentes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public Task<bool> ActualizarAsync(Agente agente, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Agente> CrearAsync(Agente agente, CancellationToken ct)
        {

            _ctx.Agentes.Add(agente);
            try
            {
                await _ctx.SaveChangesAsync(ct);
                return agente;
            }
            catch (Exception)
            {
                throw new ConflictoException($"No se pudo crear el agente con correo {agente.Correo}. Puede que ya exista un agente con ese correo.");
            }
        }

        public Task<bool> EliminarAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultadoPaginado<Agente>> ObtenerActivosPaginadoAsync(AgenteFiltro filtro, CancellationToken ct)
        {
            // AsNoTracking: solo leemos, EF no necesita vigilar los objetos.
            IQueryable<Agente> consulta = _ctx.Agentes.AsNoTracking().Where(a => a.Activo);

            if (!string.IsNullOrWhiteSpace(filtro.Q))
            {
                var texto = filtro.Q.Trim();
                consulta = consulta.Where(a =>
                    a.Nombre.Contains(texto) ||
                    a.Correo.Contains(texto) ||
                    a.Telefono.Contains(texto));
            }

            var total = await consulta.CountAsync(ct);

            var items = await consulta
                .OrderBy(a => a.Nombre).ThenBy(a => a.Id)
                .Skip((filtro.Pagina - 1) * filtro.Tamano)
                .Take(filtro.Tamano)
                .ToListAsync(ct);

            return new ResultadoPaginado<Agente>
            {
                Items = items,
                Total = total,
                Pagina = filtro.Pagina,
                Tamano = filtro.Tamano
            };

        }
    }
}
