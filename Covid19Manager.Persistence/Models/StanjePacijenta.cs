namespace Covid19Manager.Persistence.Models
{
    public class StanjePacijenta
    {
        public long Id { get; set; }
        public long KorisnikId { get; set; }
        public decimal Temperatura { get; set; }
        public int Kasalj { get; set; }
        public int Umor { get; set; }
        public int BolUMisicima { get; set; }
        public long Vrijeme { get; set; }
    }
}
