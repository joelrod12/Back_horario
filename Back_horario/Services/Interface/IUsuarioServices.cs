using Back_horario.Models.Domain.DTO.Auth;
using Back_horario.Models.Domain.DTO.Usuario;

namespace Back_horario.Services.Interface
{
    public interface IUsuarioServices
    {
        Task<List<UsuarioDTO>> GetAll();
        public Task<UsuarioDTO> GetById(int id);
        public Task<bool> Create(UsuarioDTO request);
        public Task<bool> Update(int id, UsuarioDTO request);
        public Task<bool> Delete(int id);
        public Task<LoginResponseDTO> Login(LoginDTO login);
        public Task<bool> ChangePassword(string correo, string currentPassword, string newPassword);
        public Task<bool> ForgotPassword(string correo);
        public Task<bool> Register(RegisterDTO register);

    }
}
