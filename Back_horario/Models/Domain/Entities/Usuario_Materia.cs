using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_horario.Models.Domain.Entities
{
    public class Usuario_Materia
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Usuarios")]
        public int UsuarioId { get; set; }
        public Usuario Usuarios { get; set; } = null!;
        [Required]
        [ForeignKey("Materias")]
        public int MateriaId { get; set; }
        public Materia Materias { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    }
}
