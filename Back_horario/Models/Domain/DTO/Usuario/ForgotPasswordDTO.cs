namespace Back_horario.Models.Domain.DTO.Usuario
{
    public class ForgotPasswordDTO
    {
        public string Correo { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string ConfirmarContraseña { get; set; } = null!;
    }
}
