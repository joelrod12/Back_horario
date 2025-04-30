using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_horario.Models.Domain.Entities
{
    public class Horario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public string Tarea { get; set; } = null!;
        public string? Salon { get; set; }
        public string? Edificio { get; set; }
        [Required]
        [ForeignKey("Grupos")]
        public int GrupoId { get; set; }
        public Grupo Grupos { get; set; } = null!;
        [Required]
        [ForeignKey("Temas")]
        public int TemaId { get; set; }
        public Tema Temas { get; set; } = null!;
        [Required]
        [ForeignKey("Usuarios")]
        public int UsuarioId { get; set; }
        public Usuario Usuarios { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();


    }
}
