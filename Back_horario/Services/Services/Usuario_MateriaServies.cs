using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Back_horario.Services.Services
{
    public class Usuario_MateriaServies : IUsuario_MateriaServies
    {
        private readonly ApplicationDbContext _context;
        public Usuario_MateriaServies(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Usuario_MateriaDTO request)
        {
            try
            {
                var usuarioMateria = new Usuario_Materia
                {
                    UsuarioId = request.UsuarioId,
                    MateriaId = request.MateriaId
                };
                await _context.Usuario_Materias.AddAsync(usuarioMateria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario materia", ex);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var usuarioMateria = await _context.Usuario_Materias.FindAsync(id)
                    ?? throw new Exception("Usuario materia no encontrado");
                _context.Usuario_Materias.Remove(usuarioMateria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el usuario materia: {ex.Message}", ex);
            }
        }
        public async Task<List<Usuario_MateriaDTO>> GetAll()
        {
            try
            {
                return await _context.Usuario_Materias
                    .Select(um => new Usuario_MateriaDTO
                    {
                        Id = um.Id,
                        UsuarioId = um.UsuarioId,
                        MateriaId = um.MateriaId,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios materias", ex);
            }
        }

    }
}
