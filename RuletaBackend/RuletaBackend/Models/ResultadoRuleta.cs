namespace RuletaBackend.Models
{
    public class ResultadoRuleta
    {
        public int Numero { get; set; }
        public string Color { get; set; } = string.Empty;

        public bool EsPar => Numero != 0 && Numero % 2 == 0;
        public bool EsImpar => Numero % 2 != 0;
    }
}
