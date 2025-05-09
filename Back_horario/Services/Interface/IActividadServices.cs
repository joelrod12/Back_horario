using Back_horario.Models.Domain.DTO;

namespace Back_horario.Services.Interface
{
    public interface IActividadServices
    {
        Task<List<ActividadDTO>> GetAll();
        Task<ActividadDTO> GetById(int id);
        Task<bool> Create(ActividadDTO request);
        Task<bool> Update(int id, ActividadDTO request);
        Task<bool> Delete(int id);
        Task<List<ActividadDTO>> GetByTemaId(int temaId);
    }
}
