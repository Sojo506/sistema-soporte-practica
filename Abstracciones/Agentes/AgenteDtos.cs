using Abstracciones.Comunes;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Agentes
{
    public class CrearAgenteDto
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int CapacidadMaxima { get; set; }
    }

    public class ActualizarAgenteDto : CrearAgenteDto
    {
        public int Id { get; set; }
    }

    /// <summary>Salida hacia el cliente HTTP. Nunca se expone la entidad directamente.</summary>
    public record AgenteDto(
        int Id,
        string Nombre,
        string Correo,
        string Telefono,
        int CapacidadMaxima,
        bool Activo,
        DateTime FechaRegistro);
}

public class AgenteFiltro : ParametrosPaginacion
{
    /// <summary>Texto buscado en nombre, correo o empresa.</summary>
    [StringLength(100)]
    public string? Q { get; set; }
}
