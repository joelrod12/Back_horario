namespace Back_horario.Models.Domain.DTO.Auth
{
    public class LoginResponseDTO
    {
        public int  Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
