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


        [HttpPost]
        public async Task<ActionResult<Repuesto>> Post(Repuesto repuestoNuevo)
        {
            if (string.IsNullOrEmpty(repuestoNuevo.Nombre) || repuestoNuevo.Precio <= 0)
            {
                return BadRequest(new { mensaje = "Datos inválidos. El nombre es obligatorio y el precio debe ser mayor a 0." });
            }

            int nuevoId = await _repository.Insertar(repuestoNuevo);
            repuestoNuevo.Id = nuevoId;

            return CreatedAtAction(nameof(GetById), new { id = repuestoNuevo.Id }, repuestoNuevo);

        }


    }
}
