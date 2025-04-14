using RuletaBackend.Context;
using RuletaBackend.Models;

namespace RuletaBackend.Services
{
    public class RuletaService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        private readonly Random _random = new();

        public ResultadoRuleta Girar()
        {
            int numero = _random.Next(0, 37);
            string color = (numero == 0) ? "verde" : (numero % 2 == 0 ? "negro" : "rojo");

            return new ResultadoRuleta
            {
                Numero = numero,
                Color = color
            };
        }

        public ResultadoApuesta ProcesarApuesta(Apuesta apuesta)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre.ToLower() == apuesta.NombreUsuario.ToLower());

            // Crear resultado del giro
            var resultado = new ResultadoRuleta
            {
                Numero = apuesta.NumeroObtenido,
                Color = apuesta.ColorObtenido
            };

            bool gano = false;
            decimal premio = 0;

            // Evaluación de la apuesta
            if (apuesta.Numero.HasValue && apuesta.Color != null)
            {
                if (apuesta.Numero == resultado.Numero && apuesta.Color.ToLower() == resultado.Color.ToLower())
                {
                    gano = true;
                    premio = apuesta.Monto * 3;
                }
            }
            else if (apuesta.EsPar.HasValue && apuesta.Color != null)
            {
                bool esParResultado = resultado.Numero != 0 && resultado.Numero % 2 == 0;

                if (apuesta.EsPar == esParResultado && apuesta.Color.ToLower() == resultado.Color.ToLower())
                {
                    gano = true;
                    premio = apuesta.Monto;
                }
            }
            else if (apuesta.Color != null)
            {
                if (apuesta.Color.ToLower() == resultado.Color.ToLower())
                {
                    gano = true;
                    premio = apuesta.Monto / 2;
                }
            }

            decimal nuevoSaldo;
            if (gano)
                nuevoSaldo = apuesta.Monto + premio;
            else
                nuevoSaldo = 0;

            return new ResultadoApuesta
            {
                Resultado = resultado,
                Gano = gano,
                Premio = premio,
                NuevoSaldo = nuevoSaldo
            };
        }

        /*public ResultadoApuesta ProcesarApuestav1(Apuesta apuesta)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre.ToLower() == apuesta.NombreUsuario.ToLower()) ?? throw new Exception("Usuario no encontrado.");
            if (usuario.Saldo < apuesta.Monto)
                throw new Exception("Saldo insuficiente.");

            var resultado = Girar();

            bool gano = false;
            decimal premio = 0;

            if (apuesta.Numero.HasValue && apuesta.Color != null)
            {
                if (apuesta.Numero == resultado.Numero && apuesta.Color.ToLower() == resultado.Color.ToLower())
                {
                    gano = true;
                    premio = apuesta.Monto * 3;
                }
            }
            else if (apuesta.EsPar.HasValue && apuesta.Color != null)
            {
                bool esParResultado = resultado.Numero != 0 && resultado.Numero % 2 == 0;

                if (apuesta.EsPar == esParResultado && apuesta.Color.ToLower() == resultado.Color.ToLower())
                {
                    gano = true;
                    premio = apuesta.Monto;
                }
            }
            else if (apuesta.Color != null)
            {
                if (apuesta.Color.ToLower() == resultado.Color.ToLower())
                {
                    gano = true;
                    premio = apuesta.Monto / 2;
                }
            }

            if (gano)
                usuario.Saldo += premio;
            else
                usuario.Saldo -= apuesta.Monto;

            usuario.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return new ResultadoApuesta
            {
                Resultado = resultado,
                Gano = gano,
                Premio = premio,
                NuevoSaldo = usuario.Saldo
            };
        }*/

    }

}
