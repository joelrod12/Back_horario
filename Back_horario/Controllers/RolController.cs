using System.Runtime.CompilerServices;
using Back_horario.Models.Domain.DTO;
using Back_horario.Models.Domain.Entities;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolServices _rolServices;
        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<List<RolDTO>>> GetAll()
        {
            var roles = await _rolServices.GetAll();
            return Ok(roles);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetById(int id)
        {
            var rol = await _rolServices.GetById(id);
            return Ok(rol);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<RolDTO>> Create([FromBody] RolDTO rol)
        {
            try
            {
                var success = await _rolServices.Create(rol);
                return success ? Ok(new { message = "Rol creado correctamente" }) : BadRequest(new { message = "Error al crear el rol" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<RolDTO>> Update(int id, [FromBody] RolDTO rol)
        {
            try
            {
                var success = await _rolServices.Update(id, rol);
                return success ? Ok(new { message = "Rol actualizado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<RolDTO>> Delete(int id)
        {
            try
            {
                var success = await _rolServices.Delete(id);
                return success ? Ok(new { message = "Rol eliminado correctamente" }) : NotFound("Rol no encontrado o con usuarios asociados.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

