using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;

namespace Back_horario.Services.Interface
{
    public interface IRolServices
    {
        Task<bool> Create(RolDTO request);
        Task<bool> Delete(int id);
        Task<List<RolDTO>> GetAll();
        Task<RolDTO> GetById(int id);
        Task<bool> Update(int id, RolDTO request);
    }
}
