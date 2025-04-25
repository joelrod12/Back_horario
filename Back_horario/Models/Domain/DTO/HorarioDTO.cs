namespace Back_horario.Models.Domain.DTO
{
    public class HorarioDTO : Base.BaseDTO
    {
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string Tarea { get; set; } = null!;
        public string? Edificio { get; set; }
        public int GrupoId { get; set; }
        public int TemaId { get; set; }
        public int UsuarioId { get; set; }

    }
}
