using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Services.Interface;
using Back_horario.Services.Interface.Email;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Services.Services
{
    public class MateriaServices : IMateriaServices
    {
        private readonly ApplicationDbContext _context;
        public MateriaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MateriaDTO>> GetAll()
        {
            try
            {
                return await _context.Materias
                    .Select(m => new MateriaDTO
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        Color = m.Color,
                        Semestre = m.Semestre,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en obtener las materias", ex);
            }
        }
        public async Task<MateriaDTO> GetById(int id)
        {
            try
            {
                return await _context.Materias
                    .Where(m => m.Id == id)
                    .Select(m => new MateriaDTO
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        Color = m.Color,
                        Semestre = m.Semestre,
                    })
                    .FirstOrDefaultAsync() ?? throw new Exception("Materia no encontrada");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en obtener la materia: {ex.Message}", ex);
            }
        }

        public async Task<bool> Create(MateriaDTO materia)
        {
            try
            {
                // validar si ya existe una materia con el mismo nombre
                if (await _context.Materias.AnyAsync(m => m.Nombre == materia.Nombre))
                    throw new Exception("Ya existe una materia con ese nombre");
                var newMateria = new Models.Domain.Entities.Materia
                {
                    Nombre = materia.Nombre,
                    Color = materia.Color,
                    Semestre = materia.Semestre,
                };
                await _context.Materias.AddAsync(newMateria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en crear la materia: {ex.Message}", ex);
            }
        }
        public async Task<bool> Update(int id, MateriaDTO materia)
        {
            try
            {
                var existingMateria = await _context.Materias.FindAsync(id)
                    ?? throw new Exception("Materia no encontrada");
                // validar si ya existe una materia con el mismo nombre
                if (await _context.Materias.AnyAsync(m => m.Nombre == materia.Nombre && m.Id != id))               
                    throw new Exception("Ya existe una materia con ese nombre");
                

                existingMateria.Nombre = materia.Nombre;
                existingMateria.Color = materia.Color;
                existingMateria.Semestre = materia.Semestre;
                _context.Materias.Update(existingMateria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en actualizar la materia: {ex.Message}", ex);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var existingMateria = await _context.Materias.FindAsync(id)
                    ?? throw new Exception("Materia no encontrada");
                
                _context.Materias.Remove(existingMateria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en eliminar la materia: {ex.Message}", ex);
            }
        }

    }
}
