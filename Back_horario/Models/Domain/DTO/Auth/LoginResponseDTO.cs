namespace Back_horario.Models.Domain.DTO.Auth
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }         // ← Nuevo: guarda aquí el UserId
        public string Token { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
