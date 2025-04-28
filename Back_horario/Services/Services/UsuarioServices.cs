using Back_horario.Context;
using Back_horario.Models.Domain.DTO.Usuario;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Back_horario.Services.Services.Email;
using NuGet.Common;
using Back_horario.Services.Interface.Email;
using Humanizer;
using Back_horario.Models.Domain.Entities.Auth;
using Microsoft.Extensions.Options;
using Back_horario.Models.Domain.DTO.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Back_horario.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailServices _emailService;
        private readonly JwtSettings _jwtSettings;

        public UsuarioServices(ApplicationDbContext context, IEmailServices emailServices, IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _emailService = emailServices;
            _jwtSettings = jwtOptions.Value;

        }

        public async Task<List<UsuarioDTO>> GetAll()
        {
            try
            {
                return await _context.Usuarios
                    .Select(r => new UsuarioDTO
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                        Apellido = r.Apellido,
                        Correo = r.Correo,
                        Contraseña = r.Contraseña,
                        RolId = r.RolId,

                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtener los usuarios", ex);
            }
        }
        public async Task<UsuarioDTO> GetById(int id)
        {
            try
            {
                return await _context.Usuarios
                    .Where(r => r.Id == id)
                    .Select(r => new UsuarioDTO
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                        Apellido = r.Apellido,
                        Correo = r.Correo,
                        Contraseña = r.Contraseña,
                        RolId = r.RolId,
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en obtener el usuario: {ex.Message}", ex);
            }

        }
        public async Task<bool> Create(UsuarioDTO request)
        {
            try
            {
                // Validar si ya existe un usuario con el mismo correo
                if (await _context.Usuarios.AnyAsync(r => r.Correo == request.Correo))
                {
                    throw new Exception("Ya existe un usuario con ese correo.");
                }

                // Generar el hash de la contraseña
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Contraseña);

                var usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Correo = request.Correo,
                    Contraseña = BCrypt.Net.BCrypt.HashPassword(request.Contraseña),
                    RolId = request.RolId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el usuario: {ex.Message}", ex);
            }
        }


        public async Task<bool> Update(int id, UsuarioDTO request)
        {
            try
            {
                // Buscar el usuario por ID
                var usuario = await _context.Usuarios.FindAsync(id)
                    ?? throw new Exception("Usuario no encontrado");
                if (await _context.Usuarios.AnyAsync(r => r.Correo == request.Correo))
                {
                    throw new Exception("Ya existe un usuario con ese correo.");
                }

                // Actualizar los datos del usuario
                usuario.Nombre = request.Nombre;
                usuario.Apellido = request.Apellido;
                usuario.Correo = request.Correo;

                // Verificar y actualizar la contraseña si es necesario
                if (!BCrypt.Net.BCrypt.Verify(request.Contraseña, usuario.Contraseña))
                {
                    usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(request.Contraseña);
                }

                usuario.RolId = request.RolId;
                usuario.UpdatedAt = DateTime.UtcNow;

                // Guardar los cambios en la base de datos
                _context.Usuarios.Update(usuario);
                return await _context.SaveChangesAsync() > 0; // Retornar si la operación fue exitosa
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario: {ex.Message}", ex); // Lanzar la excepción al controlador
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id)
                    ?? throw new Exception("Usuario no encontrado");

                _context.Usuarios.Remove(usuario);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en eliminar el usuario: {ex.Message}", ex);
            }
        }
        public async Task<LoginResponseDTO> Login(LoginDTO login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Correo) || string.IsNullOrEmpty(login.Contraseña))
                    throw new Exception("Correo y contraseña son obligatorios");

                var usuario = await _context.Usuarios
                    .Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Correo == login.Correo);

                if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Contraseña, usuario.Contraseña))
                    throw new Exception("Usuario o contraseña incorrectos");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.RolId.ToString())
                };

                // Crear el token JWT
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new LoginResponseDTO
                {
                    Id = usuario.Id,
                    Token = tokenString,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    Rol = usuario.Roles.Nombre,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"No se logro iniciar sesion: {ex.Message}", ex);
            }
        }


        public async Task<bool> ChangePassword(string correo, string currentPassword, string newPassword)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo)
                    ?? throw new Exception("Usuario no encontrado");


                // Verificar si la contraseña actual es correcta
                if (!BCrypt.Net.BCrypt.Verify(currentPassword, usuario.Contraseña))
                {
                    throw new Exception("La contraseña actual es incorrecta");
                }

                // Validar que la nueva contraseña no sea igual a la actual
                if (BCrypt.Net.BCrypt.Verify(newPassword, usuario.Contraseña))
                {
                    throw new Exception("La nueva contraseña no puede ser igual a la actual");
                }

                // Generar el hash de la nueva contraseña
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                usuario.Contraseña = hashedPassword;  // Almacenar la contraseña como hash
                usuario.UpdatedAt = DateTime.Now;
                _context.Usuarios.Update(usuario);

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en cambiar la contraseña: {ex.Message}", ex);
            }
        }
        //forgot-password
        public async Task<bool> ForgotPassword(string correo)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo)
                  ?? throw new Exception("Usuario no encontrado");

                // Generar token y fecha de expiración
                usuario.ResetToken = Guid.NewGuid().ToString();
                usuario.ResetTokenExpiration = DateTime.UtcNow.AddMinutes(15);

                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                // Enviar el token por correo
                await _emailService.SendResetToken(correo, usuario.ResetToken, usuario.ResetTokenExpiration);

                // Para propósitos de depuración, imprime el token y la fecha de expiración
                Console.WriteLine($"Token para {correo}: {usuario.ResetToken}");
                var tiempoRestante = usuario.ResetTokenExpiration - DateTime.UtcNow;

                Console.WriteLine($"Expira en {tiempoRestante.Value.Hours} horas y {tiempoRestante.Value.Minutes} minutos.");

                // Aquí puedes agregar la lógica para enviar un correo de restablecimiento de contraseña
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en enviar el correo de restablecimiento de contraseña", ex);
            }
        }

        //registro
        public async Task<bool> Register(RegisterDTO register)
        {
            try
            {
                // Validar si ya existe un usuario con el mismo correo
                if (await _context.Usuarios.AnyAsync(r => r.Correo == register.Correo))
                {
                    throw new Exception("Ya existe un usuario con ese correo.");
                }
                if (register.Contraseña != register.ConfirmacionContraseña)
                    throw new Exception("Las contraseñas no coinciden");

                // Buscar el rol por nombre
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == "User")
                    ?? throw new Exception("Rol predeterminado no encontrado.");
                // Generar el hash de la contraseña
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Contraseña);
                var usuario = new Usuario
                {
                    Nombre = register.Nombre,
                    Apellido = register.Apellido,
                    Correo = register.Correo,
                    Contraseña = hashedPassword,
                    RolId = rol.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Usuarios.Add(usuario); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar el usuario: {ex.Message}", ex);
            }
        }
    }
}
