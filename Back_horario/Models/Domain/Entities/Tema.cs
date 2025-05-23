﻿using System.ComponentModel.DataAnnotations;
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
        public string Unidad { get; set; } = null!;
        [Required]
        [ForeignKey("Materias")]
        public int MateriaId { get; set; }
        public Materia Materias { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();

    }
}
