﻿namespace Back_horario.Models.Domain.DTO
{
    public class TemaDTO : Base.BaseDTO
    {
        public string Nombre { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Unidad { get; set; } = null!;
        public int MateriaId { get; set; }

    }
}
