using Back_horario.Models.Domain.DTO;
using Back_horario.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_horario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario_MateriaController : ControllerBase
    {
        private readonly IUsuario_MateriaServies _usuario_MateriaServices;
        public Usuario_MateriaController(IUsuario_MateriaServies usuario_MateriaServices)
        {
            _usuario_MateriaServices = usuario_MateriaServices;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<List<Usuario_MateriaDTO>>> GetAll()
        {
            var usuarioMaterias = await _usuario_MateriaServices.GetAll();
            return Ok(usuarioMaterias);
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<Usuario_MateriaDTO>> Create([FromBody] Usuario_MateriaDTO usuarioMateria)
        {
            try
            {
                var success = await _usuario_MateriaServices.Create(usuarioMateria);
                return success ? Ok(new { message = "Usuario-Materia creado correctamente" }) : BadRequest(new { message = "Error al crear el Usuario-Materia" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario_MateriaDTO>> Delete(int id)
        {
            try
            {
                var success = await _usuario_MateriaServices.Delete(id);
                return success ? Ok(new { message = "Usuario-Materia eliminado correctamente" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
