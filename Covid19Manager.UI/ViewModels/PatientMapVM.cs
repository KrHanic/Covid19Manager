using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientMapVM
    {
        [DisplayName("Pacijent")]
        public string Name { get; set; }
        [DisplayName("Posljednja latituda")]
        public string CurrentLat { get; set; }
        [DisplayName("Posljedna longituda")]
        public string CurrentLong { get; set; }
        [DisplayName("Lokacija zabilježena")]
        public string LocationTime { get; set; }
        [DisplayName("Temperatura")]
        public string Temperature { get; set; }
        [DisplayName("Kašalj")]
        public string Cough { get; set; }
        [DisplayName("Umor")]
        public string Fatigue { get; set; }
        [DisplayName("Bol u mišićima")]
        public string MusclePain { get; set; }
        [DisplayName("Stanje zabilježeno")]
        public string ConditionTime { get; set; }
        public string MapMarkerColor { get; set; }
    }
}
