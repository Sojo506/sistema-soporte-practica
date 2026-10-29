using Abstracciones.Agentes;

namespace Flujo.Agentes
{
    public static class AgenteMapeador
    {
        public static AgenteDto ADto(Agente a) =>
            new(a.Id, a.Nombre, a.Correo, a.Telefono, a.CapacidadMaxima, a.Activo, a.FechaRegistro);
    }
}
