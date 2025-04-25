using Back_horario.Models.Domain.DTO;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioServices _horarioServices;
        public HorarioController(IHorarioServices horarioServices)
        {
            _horarioServices = horarioServices;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<List<HorarioDTO>>> GetAll()
        {
            var horarios = await _horarioServices.GetAll();
            return Ok(horarios);
        }
        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDTO>> GetById(int id)
        {
            var horario = await _horarioServices.GetById(id);
            return Ok(horario);
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<HorarioDTO>> Create([FromBody] HorarioDTO horario)
        {
            try
            {
                var success = await _horarioServices.Create(horario);
                return success ? Ok(new { message = "Horario creado correctamente" }) : BadRequest(new { message = "Error al crear el horario" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<HorarioDTO>> Update(int id, [FromBody] HorarioDTO horario)
        {
            try
            {
                var success = await _horarioServices.Update(id, horario);
                return success ? Ok(new { message = "Horario actualizado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<HorarioDTO>> Delete(int id)
        {
            try
            {
                var success = await _horarioServices.Delete(id);
                return success ? Ok(new { message = "Horario eliminado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
