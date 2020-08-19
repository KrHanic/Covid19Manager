using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientTableVM
    {
        public int ID { get; set; }
        public long OIB { get; set; }
        [DisplayName("Ime")]
        public string FirstName { get; set; }
        [DisplayName("Prezime")]
        public string LastName { get; set; }
        [DisplayName("Adresa samoizolacije")]
        public string IsolationAddress { get; set; }
        public string RowColor { get; set; }
    }
}
