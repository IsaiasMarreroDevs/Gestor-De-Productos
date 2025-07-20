using Microsoft.EntityFrameworkCore;

namespace GestorProductosApi.Models;

// Contexto de Entity Framework Core que representa la conexión con la base de datos.
// Aquí se definen las tablas y configuraciones del modelo.
public partial class PruebaContext : DbContext
{
    public PruebaContext()
    {
    }

    public PruebaContext(DbContextOptions<PruebaContext> options)
        : base(options)
    {
    }

    // Propiedad que representa la tabla Productos en la base de datos
    public virtual DbSet<Producto> Productos { get; set; }

    // Configura la conexión a SQL Server con la cadena de conexión
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IMBJR25;Database=Prueba;Trusted_Connection=True;TrustServerCertificate=True");

    // Configura las propiedades y restricciones del modelo Producto
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(p => p.ProductoId); // Define ProductoId como clave primaria

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)       // Longitud máxima 50 caracteres
                .IsUnicode(false);      // No permite caracteres Unicode

            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 3)"); 

            entity.Property(e => e.ProductoId)
                .ValueGeneratedOnAdd()  // El ID se genera automáticamente al insertar
                .HasColumnName("ProductoID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

