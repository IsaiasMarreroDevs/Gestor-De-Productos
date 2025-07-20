using GestorProductosApi.Models;

namespace GestorProductosApi.Services
{
    // Servicio encargado de la lógica de negocio para gestionar productos.
    // Este servicio interactúa con el contexto de la base de datos para realizar
    // operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre la entidad Producto.
    public class ProductoService
    {
        private readonly PruebaContext _context;

        // Constructor que recibe el contexto de base de datos para realizar operaciones
        public ProductoService(PruebaContext context)
        {
            _context = context;
        }

        // Obtiene la lista completa de productos almacenados en la base de datos.
        public List<Producto> ObtenerTodos() => _context.Productos.ToList();

        // Busca un producto específico por su ID.
        public Producto? ObtenerPorId(int producto_id) =>
            _context.Productos.FirstOrDefault(p => p.ProductoId == producto_id);

        // Agrega un nuevo producto a la base de datos y guarda los cambios.
        public Producto Agregar(Producto nuevo)
        {
            _context.Add(nuevo);
            _context.SaveChanges();
            return nuevo;
        }

        // Actualiza los datos de un producto existente identificado por su ID.
        // Retorna false si el producto no existe.
        public bool Actualizar(int producto_id, Producto actualizado)
        {
            var producto = ObtenerPorId(producto_id);
            if (producto == null) return false;

            producto.Nombre = actualizado.Nombre;
            producto.Precio = actualizado.Precio;
            producto.FechaCreacion = actualizado.FechaCreacion;

            _context.SaveChanges();
            return true;
        }

        // Elimina un producto de la base de datos por su ID.
        // Retorna false si el producto no existe.
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

