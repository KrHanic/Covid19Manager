using Covid19Manager.Business.Common;
using Covid19Manager.UI.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientDashboardVM
    {
        [DisplayName("U samoizolaciji")]
        public string NumberOfPeopleInIsolation { get; set; }
        [DisplayName("Napustilo samoizolaciju")]
        public string NumberOfPeopleOutOfIsolation { get; set; }
        [DisplayName("Osoba sa simptomima")]
        public string NumberOfPeopleWithSymptoms { get; set; }
        [DisplayName("Prekršilo samoizolaciju")]
        public string NumberOfPeopleOutOfOneKmRadius { get; set; }
        [DisplayName("Pacijenti koji su napuštali lokaciju")]
        public List<PatientReportTableVM> PatientsOutOfOneKmRadius { get; set; }
        [DisplayName("Pacijenti s prijavljenim simptomima")]
        public List<PatientReportTableVM> PatientsWithSymptoms { get; set; }
        public PatientFilter Filter { get; set; }
        public int ReportTableId { get; set; } = (int)PatientReportTables.PatientsOutOfOneKmRadius;
        public bool IsJustCreated { get; set; } = true;
        public List<SelectListItem> Reports { get; set; } = new List<SelectListItem>() {
            new SelectListItem {Text = "koji su prekršili samoizolaciju", Value = "1"},
            new SelectListItem {Text = "s prijavljenim simptomima", Value = "2"}
        };
    }
}
