using Back_horario.Context;
using Back_horario.Models.Domain.DTO.Auth;
using Back_horario.Models.Domain.DTO.Usuario;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Contexto de la base de datos

        private readonly IUsuarioServices _usuarioServices;
        public UsuarioController(ApplicationDbContext context, IUsuarioServices usuarioServices)
        {
            _context = context; // Inicializar el _context
            _usuarioServices = usuarioServices;
        }
        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _usuarioServices.GetAll();
            return Ok(usuarios);
        }
        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuarioServices.GetById(id);
            return Ok(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var success = await _usuarioServices.Create(usuario);
                return success ? Ok(new { message = "Usuario creado correctamente" }) : BadRequest(new { message = "Error al crear el usuario" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Update(int id, [FromBody] UsuarioDTO usuario)
        {
            try
            {
                var success = await _usuarioServices.Update(id, usuario); // Solo delegar al servicio
                return success ? Ok(new { message = "Usuario actualizado correctamente" }) : BadRequest(new { message = "Error al actualizar el usuario" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Delete(int id)
        {
            try
            {
                var success = await _usuarioServices.Delete(id);
                return success ? Ok(new { message = "Usuario eliminado correctamente" }) : NotFound(new { message = "Usuario no encontrado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Usuario/login
        [HttpPost("Auth/Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO login)
        {
            try
            {
                var usuario = await _usuarioServices.Login(login);
                return Ok(new
                {
                    message = "Inicio de sesión exitoso",
                    usuario
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // POST: api/Usuario/change-password
        [HttpPost("Auth/Change-Password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            try
            {
                await _usuarioServices.ChangePassword(changePassword.Correo, changePassword.Contraseña, changePassword.NuevaContraseña);
                return Ok(new { message = "Contraseña cambiada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // POST: api/Usuario/forgot-password
        [HttpPost("Auth/Forgot-Password")]
        public async Task<ActionResult<bool>> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
        {
            try
            {
                var success = await _usuarioServices.ForgotPassword(forgotPassword.Correo);
                return success ? Ok(new { message = "Correo de restablecimiento de contraseña enviado" }) : BadRequest(new { message = "Error al enviar el correo de restablecimiento de contraseña" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Auth/Reset-Password")]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] ForgotPasswordDTO dto)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u =>
                    u.Correo == dto.Correo &&
                    u.ResetToken == dto.Token &&
                    u.ResetTokenExpiration > DateTime.UtcNow);

                if (usuario == null)
                    return BadRequest(new { message = "Token inválido o expirado" });

                if (dto.Contraseña != dto.ConfirmarContraseña)
                    return BadRequest(new { message = "Las contraseñas no coinciden" });

                // Actualizar contraseña y limpiar el token
                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(dto.Contraseña);
                usuario.ResetToken = null;
                usuario.ResetTokenExpiration = null;
                usuario.UpdatedAt = DateTime.UtcNow;

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Contraseña restablecida correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Register
        [HttpPost("Auth/Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                var resultado = await _usuarioServices.Register(dto);
                return Ok(new { message = "Usuario registrado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }

}
