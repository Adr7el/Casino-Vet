namespace RuletaBackend.Models
{
    public class ResultadoApuesta
    {
        public ResultadoRuleta Resultado { get; set; } = new(); // número y color obtenido
        public bool Gano { get; set; }
        public decimal Premio { get; set; }
        public decimal NuevoSaldo { get; set; }
    }
}
