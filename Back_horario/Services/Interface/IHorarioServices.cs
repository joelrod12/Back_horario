using Back_horario.Models.Domain.DTO;

namespace Back_horario.Services.Interface
{
    public interface IHorarioServices
    {
        Task<bool> Create(HorarioDTO request);
        Task<bool> Delete(int id);
        Task<List<HorarioDTO>> GetAll();
        Task<HorarioDTO> GetById(int id);
        Task<bool> Update(int id, HorarioDTO request);
    }
}
