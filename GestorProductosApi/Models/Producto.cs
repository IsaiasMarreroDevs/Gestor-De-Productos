using System;
using System.ComponentModel.DataAnnotations;

namespace GestorProductosApi.Models;

// Modelo que representa la entidad Producto.
// Contiene las propiedades que corresponden a las columnas de la tabla en la base de datos.
public partial class Producto
{
    [Key] 
    public int ProductoId { get; set; }


    public string Nombre { get; set; } = null!;

  
    public decimal Precio { get; set; }

    
    public DateTime FechaCreacion { get; set; }
}

