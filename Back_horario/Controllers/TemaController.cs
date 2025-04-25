using Back_horario.Models.Domain.DTO;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly ITemaServices _temaServices;
        public TemaController(ITemaServices temaServices)
        {
            _temaServices = temaServices;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<List<TemaDTO>>> GetAll()
        {
            var temas = await _temaServices.GetAll();
            return Ok(temas);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<TemaDTO>> GetById(int id)
        {
            var tema = await _temaServices.GetById(id);
            return Ok(tema);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<TemaDTO>> Create([FromBody] TemaDTO tema)
        {
            try
            {
                var success = await _temaServices.Create(tema);
                return success ? Ok(new { message = "Tema creado correctamente" }) : BadRequest(new { message = "Error al crear el tema" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<TemaDTO>> Update(int id, [FromBody] TemaDTO tema)
        {
            try
            {
                var success = await _temaServices.Update(id, tema);
                return success ? Ok(new { message = "Tema actualizado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<TemaDTO>> Delete(int id)
        {
            try
            {
                var success = await _temaServices.Delete(id);
                return success ? Ok(new { message = "Tema eliminado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
