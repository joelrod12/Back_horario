namespace Back_horario.Models.Domain.DTO
{
    public class HorarioDTO : Base.BaseDTO
    {
        public DateTime Fecha { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Descripcion { get; set; }
        public string Tarea { get; set; } = null!;
        public string? Salon { get; set; }
        public string? Edificio { get; set; }
        public int GrupoId { get; set; }
        public int Usuario_MateriaId { get; set; }

        // Datos adicionales para el frontend
        public string GrupoNombre { get; set; } = null!;
        public string MateriaNombre { get; set; } = null!;
        public string UsuarioNombre { get; set; } = null!;
        public string TemaNombre { get; set; } = null!;


    }
}
