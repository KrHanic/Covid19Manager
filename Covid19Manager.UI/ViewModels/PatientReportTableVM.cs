using System.ComponentModel;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientReportTableVM
    {
        public string OIB { get; set; }
        [DisplayName("Ime")]
        public string FirstName { get; set; }
        [DisplayName("Prezime")]
        public string LastName { get; set; }
        [DisplayName("Adresa samoizolacije")]
        public string IsolationAddress { get; set; }
    }
}