using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuletaBackend.Context;
using RuletaBackend.Models;
using RuletaBackend.Services;

namespace RuletaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuletaController(AppDbContext context, RuletaService ruletaService) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly RuletaService _ruletaService = ruletaService;

        // GET api/Ruleta/girar
        [HttpGet("girar")]
        public ActionResult<ResultadoRuleta> GirarRuleta()
        {
            var resultado = _ruletaService.Girar();
            return Ok(resultado);
        }

        // POST api/Ruleta/apostar
        [HttpPost("apostar")]
        public ActionResult<ResultadoApuesta> Apostar([FromBody] Apuesta apuesta)
        {
            try
            {
                var resultado = _ruletaService.ProcesarApuesta(apuesta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
