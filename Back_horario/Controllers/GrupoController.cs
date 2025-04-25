using Back_horario.Models.Domain.DTO;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoServices _grupoServices;
        public GrupoController(IGrupoServices grupoServices)
        {
            _grupoServices = grupoServices;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<List<GrupoDTO>>> GetAll()
        {
            var grupos = await _grupoServices.GetAll();
            return Ok(grupos);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoDTO>> GetById(int id)
        {
            var grupo = await _grupoServices.GetById(id);
            return Ok(grupo);
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<GrupoDTO>> Create([FromBody] GrupoDTO grupo)
        {
            try
            {
                var success = await _grupoServices.Create(grupo);
                return success ? Ok(new { message = "Grupo creado correctamente" }) : BadRequest(new { message = "Error al crear el grupo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<GrupoDTO>> Update(int id, [FromBody] GrupoDTO grupo)
        {
            try
            {
                var success = await _grupoServices.Update(id, grupo);
                return success ? Ok(new { message = "Grupo actualizado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<GrupoDTO>> Delete(int id)
        {
            try
            {
                var success = await _grupoServices.Delete(id);
                return success ? Ok(new { message = "Grupo eliminado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
