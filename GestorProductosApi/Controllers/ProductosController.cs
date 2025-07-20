using Microsoft.AspNetCore.Mvc;
using GestorProductosApi.Models;
using GestorProductosApi.Services;

namespace GestorProductosApi.Controllers
{
    // Controlador API para manejar las solicitudes HTTP relacionadas con productos.
    // Define las rutas y métodos para consumir los servicios RESTful.
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _servicio;

        // Inyección de dependencias del servicio de productos
        public ProductosController(ProductoService servicio)
        {
            _servicio = servicio;
        }

        // GET api/productos
        // Retorna la lista de todos los productos
        [HttpGet]
        public ActionResult<List<Producto>> Get() => _servicio.ObtenerTodos();

        // GET api/productos/{producto_id}
        // Retorna un producto específico por su ID o NotFound si no existe
        [HttpGet("{producto_id}")]
        public ActionResult<Producto> Get(int producto_id)
        {
            var producto = _servicio.ObtenerPorId(producto_id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        // POST api/productos
        // Agrega un nuevo producto y retorna el producto creado con su ID asignado
        [HttpPost]
        public ActionResult<Producto> Post([FromBody] Producto producto)
        {
            var nuevo = _servicio.Agregar(producto);
            // Retorna código 201 Created con la ruta para consultar el producto nuevo
            return CreatedAtAction(nameof(Get), new { id = nuevo.ProductoId }, nuevo);
        }

        // PUT api/productos/{producto_id}
        // Actualiza un producto existente, retorna 204 NoContent o 404 NotFound si no existe
        [HttpPut("{producto_id}")]
        public IActionResult Put(int producto_id, [FromBody] Producto producto)
        {
            var actualizado = _servicio.Actualizar(producto_id, producto);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        // DELETE api/productos/{producto_id}
        // Elimina un producto por ID, retorna 204 NoContent o 404 NotFound si no existe
        [HttpDelete("{producto_id}")]
        public IActionResult Delete(int producto_id)
        {
            var eliminado = _servicio.Eliminar(producto_id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}

