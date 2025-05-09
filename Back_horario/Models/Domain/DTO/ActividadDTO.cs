namespace Back_horario.Models.Domain.DTO
{
    public class ActividadDTO : Base.BaseDTO
    {
        public string Descripcion { get; set; } = null!;
        public int TemaId { get; set; }
    }
}
