using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_horario.Models.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Apellido { get; set; } = null!;

        [Required]
        public string Correo { get; set; } = null!;

        [Required]
        public string Contraseña { get; set; } = null!;

        [Required]
        [ForeignKey("Roles")]
        public int RolId { get; set; }
        public Rol Roles { get; set; } = null!;
        public string? ResetToken { get; set; } // Token de recuperación
        public DateTime? ResetTokenExpiration { get; set; } // Expiración


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
        public ICollection<Tema> Temas { get; set; } = new List<Tema>();
    }
}
