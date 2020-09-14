using System.Collections.Generic;

namespace Covid19Manager.Persistence.Models
{
    public class PacijentReadDTO
    {
        public long Id { get; set; }
        public long Oib { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string AdresaSi { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public LokacijaPacijenta ZadnjaLokacija { get; set; }
        public StanjePacijenta ZadnjeStanje { get; set; }
        public List<StanjePacijenta> PovijestStanja { get; set; }
    }
}
