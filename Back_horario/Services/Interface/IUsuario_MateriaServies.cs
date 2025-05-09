using Back_horario.Models.Domain.DTO;

namespace Back_horario.Services.Interface
{
    public interface IUsuario_MateriaServies
    {
        Task<bool> Create(Usuario_MateriaDTO request);
        Task<bool> Delete(int id);
        Task<List<Usuario_MateriaDTO>> GetAll();
    }
}
