using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Services.Services
{
    public class HorarioServices : IHorarioServices
    {
        private readonly ApplicationDbContext _context;
        public HorarioServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HorarioDTO>> GetAll()
        {
            try
            {
                return await _context.Horarios
                    .Select(h => new HorarioDTO
                    {
                        Id = h.Id,
                        Fecha = h.Fecha,
                        Descripcion = h.Descripcion,
                        Tarea = h.Tarea,
                        Edificio = h.Edificio,
                        GrupoId = h.GrupoId,
                        TemaId = h.TemaId,
                        UsuarioId = h.UsuarioId,

                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtener los horarios", ex);
            }
        }

        public async Task<HorarioDTO> GetById(int id)
        {
            try
            {
                return await _context.Horarios
                    .Where(h => h.Id == id)
                    .Select(h => new HorarioDTO
                    {
                        Id = h.Id,
                        Fecha = h.Fecha,
                        Descripcion = h.Descripcion,
                        Tarea = h.Tarea,
                        Edificio = h.Edificio,
                        GrupoId = h.GrupoId,
                        TemaId = h.TemaId,
                        UsuarioId = h.UsuarioId
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Horario no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en obtener el horario: {ex.Message}", ex);
            }
        }

        public async Task<bool> Create(HorarioDTO request)
        {
            try
            {
                // Validar si ya existe un horario con la misma fecha y tarea
                if (await _context.Horarios.AnyAsync(h => h.Fecha == request.Fecha && h.Tarea == request.Tarea))
                {
                    throw new Exception("Ya existe un horario con esa fecha y tarea.");
                }
                var horario = new Horario
                {
                    Fecha = request.Fecha,
                    Descripcion = request.Descripcion,
                    Tarea = request.Tarea,
                    Edificio = request.Edificio,
                    GrupoId = request.GrupoId,
                    TemaId = request.TemaId,
                    UsuarioId = request.UsuarioId
                };
                await _context.Horarios.AddAsync(horario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el horario: {ex.Message}", ex);
            }
        }

        public async Task<bool> Update(int id, HorarioDTO request)
        {
            try
            {
                var horario = await _context.Horarios.FindAsync(id)
                    ?? throw new Exception("Horario no encontrado");

                // Validar si ya existe un horario con la misma fecha y tarea
                if (await _context.Horarios.AnyAsync(h => h.Fecha == request.Fecha && h.Tarea == request.Tarea && h.Id != id))
                {
                    throw new Exception("Ya existe un horario con esa fecha y tarea.");
                }
                horario.Fecha = request.Fecha;
                horario.Descripcion = request.Descripcion;
                horario.Tarea = request.Tarea;
                horario.Edificio = request.Edificio;
                horario.GrupoId = request.GrupoId;
                horario.TemaId = request.TemaId;
                horario.UsuarioId = request.UsuarioId;
                _context.Horarios.Update(horario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el horario: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var horario = await _context.Horarios.FindAsync(id)
                    ?? throw new Exception("Horario no encontrado");

                _context.Horarios.Remove(horario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el horario: {ex.Message}", ex);
            }

        }
    }
}
