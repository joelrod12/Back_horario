using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace Back_horario.Services.Services
{
    public class ActividadServices : IActividadServices
    {
        private readonly ApplicationDbContext _context;

        public ActividadServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ActividadDTO>> GetAll()
        {
            try
            {
                return await _context.Actividades
                    .Select(a => new ActividadDTO
                    {
                        Id = a.Id,
                        Descripcion = a.Descripcion,
                        TemaId = a.TemaId,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener las actividades", ex);
            }
        }

        public async Task<ActividadDTO> GetById(int id)
        {
            try
            {
                return await _context.Actividades
                    .Where(a => a.Id == id)
                    .Select(a => new ActividadDTO
                    {
                        Id = a.Id,
                        Descripcion = a.Descripcion,
                        TemaId = a.TemaId,
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Actividad no encontrada");
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception($"Error al obtener la actividad: {ex.Message}", ex);
            }
        }

        public async Task<bool> Create(ActividadDTO request)
        {
            try
            {
                // Validar si ya existe una actividad con la misma descripción
                if (await _context.Actividades.AnyAsync(a => a.Descripcion == request.Descripcion))
                {
                    throw new Exception("Ya existe una actividad con esa descripción");
                }
                var actividad = new Actividad
                {
                    Descripcion = request.Descripcion,
                    TemaId = request.TemaId,
                };
                await _context.Actividades.AddAsync(actividad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception($"Error al crear la actividad: {ex.Message}", ex);
            }
        }

        public async Task<bool> Update(int id, ActividadDTO request)
        {
            try
            {
                var actividad = await _context.Actividades.FindAsync(id)
                    ?? throw new Exception("Actividad no encontrada");

                // Validar si ya existe una actividad con la misma descripción
                if (await _context.Actividades.AnyAsync(a => a.Descripcion == request.Descripcion && a.Id != id))
                {
                    throw new Exception("Ya existe una actividad con esa descripción");
                }
                actividad.Descripcion = request.Descripcion;
                actividad.TemaId = request.TemaId;
                _context.Actividades.Update(actividad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception($"Error al actualizar la actividad: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var actividad = await _context.Actividades.FindAsync(id)
                    ?? throw new Exception("Actividad no encontrada");
                _context.Actividades.Remove(actividad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception($"Error al eliminar la actividad: {ex.Message}", ex);
            }
        }

        // buscar por tema
        public async Task<List<ActividadDTO>> GetByTemaId(int temaId)
        {
            try
            {
                return await _context.Actividades
                    .Where(a => a.TemaId == temaId)
                    .Select(a => new ActividadDTO
                    {
                        Id = a.Id,
                        Descripcion = a.Descripcion,
                        TemaId = a.TemaId,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception($"Error al obtener las actividades por tema: {ex.Message}", ex);
            }
        }

    }
}