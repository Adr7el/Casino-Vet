using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuletaBackend.Context;
using RuletaBackend.Models;

namespace RuletaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET api/Usuarios/Nombre
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(string nombre)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre.ToLower() == nombre.ToLower());

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            return Ok(usuario);
        }

        // POST api/Usuarios/guardar
        [HttpPost("guardar")]
        public async Task<IActionResult> GuardarSaldo([FromBody] Usuario usuarioRequest)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre.ToLower() == usuarioRequest.Nombre.ToLower());

            if (usuario == null)
            {
                _context.Usuarios.Add(new Usuario
                {
                    Nombre = usuarioRequest.Nombre,
                    Saldo = usuarioRequest.Saldo
                });
            }
            else
            {
                usuario.Saldo = usuarioRequest.Saldo;
                usuario.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return Ok("Saldo guardado correctamente");
        }
    }
}
