using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_horario.Models.Domain.Entities
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        public string Semestre { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Usuario_Materia> Usuario_Materias { get; set; } = new List<Usuario_Materia>();
        public ICollection<Tema> Temas { get; set; } = new List<Tema>();


    }
}
