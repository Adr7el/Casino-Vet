using RuletaBackend.Context;
using RuletaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace RuletaBackend.Seeders
{
    public static class UsuarioInicial
    {
        public static async Task SeedAsync(AppDbContext context)
        {

            if (!await context.Usuarios.AnyAsync())
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = "Gambito",
                    Saldo = 10000m,
                    CreatedAt = DateTime.UtcNow
                };

                context.Usuarios.Add(nuevoUsuario);
                await context.SaveChangesAsync();

                Console.WriteLine($"Usuario '{nuevoUsuario.Nombre}' creado.");
            }
            else
            {
                Console.WriteLine("Ya hay usuarios en la base de datos. No se creó nada.");
            }
        }
    }
}
