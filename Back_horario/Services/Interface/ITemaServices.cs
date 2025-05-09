using Back_horario.Models.Domain.DTO;

namespace Back_horario.Services.Interface
{
    public interface ITemaServices
    {
        Task<bool> Create(TemaDTO request);
        Task<bool> Delete(int id);
        Task<List<TemaDTO>> GetAll();
        Task<TemaDTO> GetById(int id);
        Task<bool> Update(int id, TemaDTO request);
    }
}
