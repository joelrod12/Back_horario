namespace Back_horario.Models.Domain.DTO
{
    public class MateriaDTO :Base.BaseDTO
    {
        public string Nombre { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Semestre { get; set; } = null!;
    }
}
