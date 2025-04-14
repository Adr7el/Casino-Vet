namespace RuletaBackend.Models
{
    public class Apuesta
    {
        public string NombreUsuario { get; set; } = string.Empty;

        // Tipo de apuesta
        public string? Color { get; set; }
        public bool? EsPar { get; set; }
        public int? Numero { get; set; }
        public decimal Monto { get; set; }

        // Resultado del giro desde el Frontend
        public int NumeroObtenido { get; set; }
        public string ColorObtenido { get; set; } = string.Empty;
    }
}
