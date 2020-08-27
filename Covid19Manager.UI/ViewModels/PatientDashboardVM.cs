using Covid19Manager.Business.Common;
using Covid19Manager.UI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientDashboardVM
    {
        [DisplayName("Broj osoba u samoizolaciji")]
        public string NumberOfPeopleInIsolation { get; set; }
        [DisplayName("Broj osoba koje su iyašle iz samoizolacije")]
        public string NumberOfPeopleOutOfIsolation { get; set; }
        [DisplayName("Broj osoba s prijavljenim simptomima")]
        public string NumberOfPeopleWithSymptoms { get; set; }
        [DisplayName("Broj osoba koje su napuštale lokaciju samoizolacije")]
        public string NumberOfPeopleOutOfOneKmRadius { get; set; }
        [DisplayName("Pacijenti koji su napuštali lokaciju")]
        public List<PatientReportTableVM> PatientsOutOfOneKmRadius { get; set; }
        [DisplayName("Pacijenti s prijavljenim simptomima")]
        public List<PatientReportTableVM> PatientsWithSymptoms { get; set; }
        public PatientFilter Filter { get; set; }
        public int ReportTableId { get; set; } = (int)PatientReportTables.PatientsOutOfOneKmRadius;
        public bool IsJustCreated { get; set; } = true;
    }
}
