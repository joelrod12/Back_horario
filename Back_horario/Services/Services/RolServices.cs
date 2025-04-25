using Azure.Core;
using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDbContext _context;
        public RolServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RolDTO>> GetAll()
        {
            try
            {
                return await _context.Roles
                    .Select(r => new RolDTO
                    {
                        Id = r.Id,
                        Nombre = r.Nombre
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtener los roles", ex);
            }
        }
        public async Task<RolDTO> GetById(int id)
        {
            try
            {
                return await _context.Roles
                    .Where(r => r.Id == id)
                    .Select(r => new RolDTO
                    {
                        Id = r.Id,
                        Nombre = r.Nombre
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Rol no encontrado");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en obtener el rol: {ex.Message}", ex);
            }
        }
        public async Task<bool> Create(RolDTO request)
        {
            try
            {
                // Validar si ya existe un rol con el mismo nombre
                if (await _context.Roles.AnyAsync(r => r.Nombre == request.Nombre))
                {
                    throw new Exception("Ya existe un rol con ese nombre.");
                }

                var rol = new Rol
                {
                    Nombre = request.Nombre,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Roles.Add(rol);
                return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en crear el rol: {ex.Message}", ex);
            }
        }
        public async Task<bool> Update(int id, RolDTO request)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id)
                    ?? throw new Exception("Rol no encontrado");

                if (request.Id != rol.Id)
                    throw new Exception ("El ID de la URL no coincide con el del objeto" );

                rol.Nombre = request.Nombre;
                rol.UpdatedAt = DateTime.Now;
                _context.Roles.Update(rol);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en actualizar el rol: {ex.Message}", ex);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(u => u.Id == id)
                    ?? throw new Exception("Rol no encontrado");

                var usuariosAsociados = await _context.Usuarios.AnyAsync(x => x.RolId == id);
                if (usuariosAsociados)
                {
                    throw new Exception("No se puede eliminar el rol porque tiene usuarios asociados");
                }
                _context.Roles.Remove(rol);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en eliminar el rol: {ex.Message}", ex);
            }
        }

    }
}
