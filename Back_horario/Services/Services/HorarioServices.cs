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
                        FechaFin = h.FechaFin,
                        Descripcion = h.Descripcion,
                        Tarea = h.Tarea,
                        Salon = h.Salon,
                        Edificio = h.Edificio,
                        GrupoId = h.GrupoId,
                        Usuario_MateriaId = h.Usuario_MateriaId,

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
                        FechaFin = h.FechaFin,
                        Descripcion = h.Descripcion,
                        Tarea = h.Tarea,
                        Salon = h.Salon,
                        Edificio = h.Edificio,
                        GrupoId = h.GrupoId,
                        Usuario_MateriaId = h.Usuario_MateriaId
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
                    FechaFin = request.FechaFin,
                    Descripcion = request.Descripcion,
                    Tarea = request.Tarea,
                    Salon = request.Salon,
                    Edificio = request.Edificio,
                    GrupoId = request.GrupoId,
                    Usuario_MateriaId = request.Usuario_MateriaId
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
                horario.FechaFin = request.FechaFin;
                horario.Descripcion = request.Descripcion;
                horario.Tarea = request.Tarea;
                horario.Salon = request.Salon;
                horario.Edificio = request.Edificio;
                horario.GrupoId = request.GrupoId;
                horario.Usuario_MateriaId = request.Usuario_MateriaId;
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

        public async Task<List<HorarioDTO>> GetByUsuarioId(int usuarioId)
        {
            try
            {
                return await _context.Horarios
                    .Include(h => h.Usuario_Materias)
                        .ThenInclude(um => um.Materias)
                    .Include(h => h.Grupos)
                    .Where(h => h.Usuario_Materias.UsuarioId == usuarioId)
                    .Select(h => new HorarioDTO
                    {
                        Id = h.Id,
                        Fecha = h.Fecha,
                        FechaFin = h.FechaFin,
                        Descripcion = h.Descripcion,
                        Tarea = h.Tarea,
                        Salon = h.Salon,
                        Edificio = h.Edificio,
                        GrupoId = h.GrupoId,
                        GrupoNombre = h.Grupos.Nombre,
                        MateriaNombre = h.Usuario_Materias.Materias.Nombre,
                        Usuario_MateriaId = h.Usuario_MateriaId
                    })
                    .ToListAsync() ?? throw new Exception("No se encontraron horarios para el usuario especificado");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener horarios por usuario", ex);
            }
        }

    }
}
