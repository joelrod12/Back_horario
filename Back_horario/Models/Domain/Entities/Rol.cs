using System.ComponentModel.DataAnnotations;

namespace Back_horario.Models.Domain.Entities
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();


    }
}
