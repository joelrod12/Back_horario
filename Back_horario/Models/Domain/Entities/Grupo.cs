using System.ComponentModel.DataAnnotations;

namespace Back_horario.Models.Domain.Entities
{
    public class Grupo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    }
}
