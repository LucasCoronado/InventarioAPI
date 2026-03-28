using Microsoft.AspNetCore.Mvc;
using InventarioAPI.Models;
using InventarioAPI.Data;

namespace InventarioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RepuestoController : ControllerBase
    {

        private readonly RepuestoRepository _repository;

        public RepuestoController(RepuestoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Repuesto>>> Get()
        {
            var lista = await _repository.ObtenerTodos();

            return Ok(lista);
            // TODO: Llamar al método del repositorio y devolver Ok() con la lista
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Repuesto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { mensaje = "El ID debe ser un número positivo mayor a cero." });
            }

            var repuestoPorId = await _repository.ObtenerPorId(id);

            if (repuestoPorId == null)
            {
                return NotFound(new { mensaje = $"El repuesto con ID {id} no existe." });
            }

            return Ok(repuestoPorId);
        }

    }
}
