using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_horario.Models.Domain.Entities
{
    public class Actividad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        [ForeignKey("Horarios")]
        public int HorarioId { get; set; }
        public Horario Horarios { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
