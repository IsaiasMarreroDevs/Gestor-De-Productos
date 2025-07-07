using Microsoft.AspNetCore.Mvc;
using GestorProductosApi.Models;
using GestorProductosApi.Services;

namespace GestorProductosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _servicio;

        public ProductosController(ProductoService servicio)
        {
            _servicio = servicio;
        }

        // Metodo de GET API/Productos
        [HttpGet]
        public ActionResult<List<Producto>> Get() => _servicio.ObtenerTodos();

        // GET API/Productos/
        [HttpGet("{producto_id}")]
        public ActionResult<Producto> Get(int producto_id)
        {
            var producto = _servicio.ObtenerPorId(producto_id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        // Metodo POST API/productos
        [HttpPost]
        public ActionResult<Producto> Post([FromBody] Producto producto)
        {
            var nuevo = _servicio.Agregar(producto);
            return CreatedAtAction(nameof(Get), new { id = nuevo.ProductoId }, nuevo);
        }

        // Metodo PUT API/Productos/
        [HttpPut("{producto_id}")]
        public IActionResult Put(int producto_id, [FromBody] Producto producto)
        {
            var actualizado = _servicio.Actualizar(producto_id, producto);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        // Metodo DELETE API/Productos/
        [HttpDelete("{producto_id}")]
        public IActionResult Delete(int producto_id)
        {
            var eliminado = _servicio.Eliminar(producto_id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
