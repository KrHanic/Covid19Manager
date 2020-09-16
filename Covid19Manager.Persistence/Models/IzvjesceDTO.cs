using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Persistence.Models
{
    public class IzvjesceDTO
    {
        public int? BrojPacijenataUIzolaciji { get; set; }
        public int? BrojPacijenataVanIzolacije { get; set; }
        public int? BrojPacijenataSaSimptomima { get; set; }
        public int? BrojPacijenataKojiSuPrekrsiliIzolaciju { get; set; }
        public List<PacijentReadDTO> Pacijenti { get; set; }
    }
}
