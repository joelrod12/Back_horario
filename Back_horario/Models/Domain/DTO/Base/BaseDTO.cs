namespace Back_horario.Models.Domain.DTO.Base
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
