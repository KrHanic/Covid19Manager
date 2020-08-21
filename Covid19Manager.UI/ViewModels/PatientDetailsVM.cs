using Covid19Manager.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientDetailsVM
    {
        public long OIB { get; set; }
        [DisplayName("Pacijent")]
        public string Name { get; set; }
        [DisplayName("Adresa samoizolacije")]
        public string IsolationAddress { get; set; }
        [DisplayName("Posljednja latituda")]
        public double CurrentLat { get; set; }
        [DisplayName("Posljedna longituda")]
        public double CurrentLong { get; set; }
        [DisplayName("Lokacija zabilježena")]
        public long LocationTime { get; set; }
        [DisplayName("Temperatura")]
        public double Temperature { get; set; }
        [DisplayName("Kašalj")]
        public bool Cough { get; set; }
        [DisplayName("Umor")]
        public bool Fatigue { get; set; }
        [DisplayName("Bol u mišićima")]
        public bool MusclePain { get; set; }
        [DisplayName("Stanje zabilježeno")]
        public long ConditionTime { get; set; }
        [DisplayName("Povijest stanja")]
        public List<Condition> ConditionHistory { get; set; }
    }
}
