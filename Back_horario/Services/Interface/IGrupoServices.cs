using Back_horario.Models.Domain.DTO;

namespace Back_horario.Services.Interface
{
    public interface IGrupoServices
    {
        public Task<bool> Create(GrupoDTO request);
        public Task<bool> Delete(int id);
        public Task<List<GrupoDTO>> GetAll();
        public Task<GrupoDTO> GetById(int id);
        public Task<bool> Update(int id, GrupoDTO request);
    }
}
    