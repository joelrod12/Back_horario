using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Services.Services
{
    public class GrupoServices : IGrupoServices
    {
        private readonly ApplicationDbContext _context;

        public GrupoServices(ApplicationDbContext context)
        {
            _context = context;
        }
    public async Task<List<GrupoDTO>> GetAll()
        {
            try
            {
                return await _context.Grupos
                    .Select(g => new GrupoDTO
                    {
                        Id = g.Id,
                        Nombre = g.Nombre,
                        Color = g.Color
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtener los grupos", ex);
            }
        }
        public async Task<GrupoDTO> GetById(int id)
        {
            try
            {
                return await _context.Grupos
                    .Where(g => g.Id == id)
                    .Select(g => new GrupoDTO
                    {
                        Id = g.Id,
                        Nombre = g.Nombre,
                        Color = g.Color
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Grupo no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en obtener el grupo: {ex.Message}", ex);
            }
        }

        public async Task<bool> Create(GrupoDTO request)
        {
            try
            {
                // Validar si ya existe un grupo con el mismo nombre
                if (await _context.Grupos.AnyAsync(g => g.Nombre == request.Nombre))
                {
                    throw new Exception("Ya existe un grupo con ese nombre");
                }
                var grupo = new Grupo
                {
                    Nombre = request.Nombre,
                    Color = request.Color
                };
                await _context.Grupos.AddAsync(grupo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en crear el grupo: {ex.Message}", ex);
            }
        }

        public async Task<bool> Update(int id, GrupoDTO request)
        {
            try
            {
                var grupo = await _context.Grupos.FindAsync(id)
                    ?? throw new Exception("Grupo no encontrado");

                if (await _context.Grupos
                    .AnyAsync(g => g.Nombre == request.Nombre && g.Id != id))
                {
                    throw new Exception("Ya existe un grupo con ese nombre");
                }

                grupo.Nombre = request.Nombre;
                grupo.Color = request.Color;
                _context.Grupos.Update(grupo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en actualizar el grupo: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var grupo = await _context.Grupos.FindAsync(id)
                    ?? throw new Exception("Grupo no encontrado");
                _context.Grupos.Remove(grupo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en eliminar el grupo: {ex.Message}", ex);
            }
        }

    }
}
