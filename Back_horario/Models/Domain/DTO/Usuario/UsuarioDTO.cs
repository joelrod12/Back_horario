namespace Back_horario.Models.Domain.DTO.Usuario
{
    public class UsuarioDTO : Base.BaseDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public int RolId { get; set; }
        public string? ResetToken { get; set; } // Token de recuperación
        public DateTime? ResetTokenExpiration { get; set; } // Expiración

    }
}
