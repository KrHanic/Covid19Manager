using System.ComponentModel.DataAnnotations;

namespace Covid19Manager.Persistence.Models
{
    public class PacijentCreateDTO
    {
        public long Oib { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string AdresaSi { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
    }
}
