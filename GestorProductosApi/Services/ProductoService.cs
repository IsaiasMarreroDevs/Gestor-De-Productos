using GestorProductosApi.Models;

namespace GestorProductosApi.Services
{
    // Simulo la base de datos con una lista en memoria
    public class ProductoService
    {
        private readonly PruebaContext _context;
        private readonly List<Producto> _productos = new();
        private int _nextProductoId = 1;

        public ProductoService(PruebaContext context)
        {
            _context = context;
        }

        //Listar Productos
        //public List<Producto> ObtenerTodos() => _productos;
        public List<Producto> ObtenerTodos() => _context.Productos.ToList();


        //Obtener Producto por ID
        public Producto? ObtenerPorId(int producto_id) =>
            _context.Productos.FirstOrDefault(p => p.ProductoId == producto_id);



        //Agragar Producto
        public Producto Agregar(Producto nuevo)
        {
         
            _context.Add(nuevo);
            _context.SaveChanges();
            
            return nuevo;
        }

        //Actualizar Productos
        public bool Actualizar(int producto_id, Producto actualizado)
        {
            var producto = ObtenerPorId(producto_id);
            if (producto == null) return false;

            producto.Nombre = actualizado.Nombre;
            producto.Precio = actualizado.Precio;
            //producto.Cantidad = actualizado.Cantidad;
            producto.FechaCreacion = actualizado.FechaCreacion;
            
            _context.SaveChanges();
            return true;
        }

        //Eliminar Productos
        public bool Eliminar(int producto_id)
        {
            var producto = ObtenerPorId(producto_id);
            if (producto == null) return false;

            _context.Remove(producto);
            _context.SaveChanges();
            return true;
        }
    }
}
