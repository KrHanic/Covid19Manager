namespace Covid19Manager.Persistence.Models
{
    public class LokacijaPacijenta
    {
        public long Id { get; set; }
        public long KorisnikId { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public long Vrijeme { get; set; }
    }
}
