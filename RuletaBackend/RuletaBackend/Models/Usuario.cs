namespace RuletaBackend.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public decimal Saldo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
