using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Services.Services
{
    public class TemaServices : ITemaServices
    {
        private readonly ApplicationDbContext _context;
        public TemaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TemaDTO>> GetAll()
        {
            try
            {
                return await _context.Temas
                    .Select(t => new TemaDTO
                    {
                        Id = t.Id,
                        Nombre = t.Nombre,
                        Color = t.Color,
                        Unidad = t.Unidad,
                        MateriaId = t.MateriaId,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtener los temas", ex);
            }
        }

        public async Task<TemaDTO> GetById(int id)
        {
            try
            {
                return await _context.Temas
                    .Where(t => t.Id == id)
                    .Select(t => new TemaDTO
                    {
                        Id = t.Id,
                        Nombre = t.Nombre,
                        Color = t.Color,
                        Unidad = t.Unidad,
                        MateriaId = t.MateriaId,
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Tema no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en obtener el tema: {ex.Message}", ex);
            }
        }

        public async Task<bool> Create(TemaDTO request)
        {
            try
            {
                // Validar si ya existe un tema con el mismo nombre
                if (await _context.Temas.AnyAsync(t => t.Nombre == request.Nombre))
                {
                    throw new Exception("Ya existe un tema con ese nombre");
                }
                var tema = new Tema
                {
                    Nombre = request.Nombre,
                    Color = request.Color,
                    Unidad = request.Unidad,
                    MateriaId = request.MateriaId,
                };
                await _context.Temas.AddAsync(tema);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en crear el tema: {ex.Message}", ex);
            }
        }

        public async Task<bool> Update(int id, TemaDTO request)
        {
            try
            {
                var tema = await _context.Temas.FindAsync(id)
                    ?? throw new Exception("Tema no encontrado");

                // Validar si ya existe un tema con el mismo nombre
                if (await _context.Temas.AnyAsync(t => t.Nombre == request.Nombre && t.Id != id))
                {
                    throw new Exception("Ya existe un tema con ese nombre");
                }
                tema.Nombre = request.Nombre;
                tema.Color = request.Color;
                tema.Unidad = request.Unidad;
                tema.MateriaId = request.MateriaId;
                _context.Temas.Update(tema);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en actualizar el tema: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var tema = await _context.Temas.FindAsync(id)
                    ?? throw new Exception("Tema no encontrado");
                
                _context.Temas.Remove(tema);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en eliminar el tema: {ex.Message}", ex);
            }
        }

    }
}
