using Back_horario.Context;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.DTO.Usuario;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly IActividadServices _actividadServices;
        public ActividadController(ApplicationDbContext context, IActividadServices actividadServices)
        {
            _actividadServices = actividadServices;
        }
        // GET: api/Actividad
        [HttpGet]
        public async Task<ActionResult<List<ActividadDTO>>> GetAll()
        {
            var actividades = await _actividadServices.GetAll();
            return Ok(actividades);
        }
        // GET: api/Actividad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActividadDTO>> GetById(int id)
        {
            var actividad = await _actividadServices.GetById(id);
            return Ok(actividad);
        }
        // POST: api/Actividad
        [HttpPost]
        public async Task<ActionResult<ActividadDTO>> Create([FromBody] ActividadDTO actividad)
        {
            try
            {
                var success = await _actividadServices.Create(actividad);
                return success ? Ok(new { message = "Actividad creada correctamente" }) : BadRequest(new { message = "Error al crear la actividad" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Actividad/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ActividadDTO>> Update(int id, [FromBody] ActividadDTO actividad)
        {
            try
            {
                var success = await _actividadServices.Update(id, actividad); // Solo delegar al servicio
                return success ? Ok(new { message = "Actividad actualizada correctamente" }) : BadRequest(new { message = "Error al actualizar la actividad" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Actividad/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActividadDTO>> Delete(int id)
        {
            try
            {
                var success = await _actividadServices.Delete(id); // Solo delegar al servicio
                return success ? Ok(new { message = "Actividad eliminada correctamente" }) : BadRequest(new { message = "Error al eliminar la actividad" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
