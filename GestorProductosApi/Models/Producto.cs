using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorProductosApi.Models;

public partial class Producto
{
    [Key]
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public DateTime FechaCreacion { get; set; }
}
