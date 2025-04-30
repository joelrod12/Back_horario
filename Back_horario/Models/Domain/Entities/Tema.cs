using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_horario.Models.Domain.Entities
{
    public class Tema
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        [ForeignKey("usuarios")]
        public int UsuarioId { get; set; }
        public Usuario Usuarios { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
    }
}
