using Abstracciones.Comunes;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Agentes
{
    public class CrearAgenteDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [StringLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe tener exactamente 8 dígitos.")]
        public string Telefono { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "La capacidad máxima debe estar entre 1 y 50.")]
        public int CapacidadMaxima { get; set; }
    }

    public class ActualizarAgenteDto : CrearAgenteDto { }

    public record AgenteDto(
        int Id,
        string Nombre,
        string Correo,
        string Telefono,
        int CapacidadMaxima,
        bool Activo,
        DateTime FechaRegistro);

    public class AgenteFiltro : ParametrosPaginacion
    {
        [StringLength(100)]
        public string? Q { get; set; }
    }
}
