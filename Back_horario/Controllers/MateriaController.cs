using Back_horario.Models.Domain.DTO;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaServices _materiaServices;
        public MateriaController(IMateriaServices materiaServices)
        {
            _materiaServices = materiaServices;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<List<MateriaDTO>>> GetAll()
        {
            var materias = await _materiaServices.GetAll();
            return Ok(materias);
        }
        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetById(int id)
        {
            var materia = await _materiaServices.GetById(id);
            return Ok(materia);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<MateriaDTO>> Create([FromBody] MateriaDTO materia)
        {
            try
            {
                var success = await _materiaServices.Create(materia);
                return success ? Ok(new { message = "Materia creada correctamente" }) : BadRequest(new { message = "Error al crear la materia" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<MateriaDTO>> Update(int id, [FromBody] MateriaDTO materia)
        {
            try
            {
                var success = await _materiaServices.Update(id, materia);
                return success ? Ok(new { message = "Materia actualizada correctamente" }) : BadRequest(new { message = "Error al actualizar la materia" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<MateriaDTO>> Delete(int id)
        {
            try
            {
                var success = await _materiaServices.Delete(id);
                return success ? Ok(new { message = "Materia eliminada correctamente" }) : NotFound(new { message = "Materia no encontrada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
