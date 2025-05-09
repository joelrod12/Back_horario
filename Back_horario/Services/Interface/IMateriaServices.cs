using Back_horario.Models.Domain.DTO;

namespace Back_horario.Services.Interface
{
    public interface IMateriaServices
    {
        Task<List<MateriaDTO>> GetAll();
        Task<MateriaDTO> GetById(int id);
        Task<bool> Create(MateriaDTO materia);
        Task<bool> Update(int id, MateriaDTO materia);
        Task<bool> Delete(int id);

    }
}
