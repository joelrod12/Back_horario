namespace Back_horario.Models.Domain.DTO.Usuario
{
    public class ChangePasswordDTO
    {
        public string Correo { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string NuevaContraseña { get; set; } = null!;
    }
}
