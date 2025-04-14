using Microsoft.EntityFrameworkCore;
using RuletaBackend.Models;

namespace RuletaBackend.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Filtro global: Para listar solo usuarios no eliminados
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => u.DeletedAt == null);

            modelBuilder.Entity<Usuario>().Property(u => u.Saldo).HasPrecision(18, 2);
            
            // Registro inicial
            /*modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Gambito",
                    Saldo = 10000m,
                    CreatedAt = DateTime.UtcNow
                }
            );*/
        }
    }
}
