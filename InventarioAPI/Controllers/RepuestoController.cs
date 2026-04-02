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
        public async Task<ActionResult<List<Repuesto>>> Get([FromQuery] string? buscar)
        {
            try
            {               
            var lista = await _repository.ObtenerTodos(buscar);
            return Ok(lista);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    title = "Error Interno del Servidor",
                    detail = ex.Message
                });
            }
            

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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Repuesto repuestoEditado)
        {

            if(id != repuestoEditado.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del objeto." });
            }

            bool exito = await _repository.Actualizar(repuestoEditado);

            if (!exito)
            {
                return NotFound(new { mensaje = $"No se encontro el repuesto con ID {id} para actualizar." });
            }

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // 1. Intentamos eliminar
            bool eliminado = await _repository.Eliminar(id);

            // 2. Si el ID no existía, avisamos con un 404
            if (!eliminado)
            {
                return NotFound(new { mensaje = $"No se pudo eliminar: el repuesto con ID {id} no existe." });
            }

            // 3. Si tuvo éxito, devolvemos 204 No Content
            // Es el estándar: "Hice lo que pediste y ya no hay nada que mostrar ahí".
            return NoContent();
        }

    }
}
