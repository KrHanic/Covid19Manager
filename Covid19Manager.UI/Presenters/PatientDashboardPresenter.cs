using Covid19Manager.Business.Common;
using Covid19Manager.Business.Entities;
using Covid19Manager.UI.Enums;
using Covid19Manager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Covid19Manager.UI.Presenters
{
    public class PatientDashboardPresenter
    {
        private PatientDashboardVM _dashboardVM;

        public PatientDashboardPresenter(PatientDashboardVM dashboardVM)
        {
            _dashboardVM = dashboardVM;
        }
        public PatientDashboardVM Present(PatientReport report)
        {
            if(_dashboardVM.IsJustCreated)
                _dashboardVM = new PatientDashboardVM() { 
                    IsJustCreated = false,
                    Filter = new PatientFilter()
                };

            _dashboardVM.PatientsOutOfOneKmRadius = new List<PatientReportTableVM>();
            _dashboardVM.PatientsWithSymptoms = new List<PatientReportTableVM>();

            SetNotFoundStrings(report, _dashboardVM);

            if(_dashboardVM.ReportTableId == (int)PatientReportTables.PatientsWithSymptoms)
                ConvertPatientListToVmList(report.Patients, _dashboardVM.PatientsWithSymptoms);
            else
                ConvertPatientListToVmList(report.Patients, _dashboardVM.PatientsOutOfOneKmRadius);

            return _dashboardVM;
        }

        private void ConvertPatientListToVmList(List<Patient> patients, List<PatientReportTableVM> patientVMs)
        {
            foreach(var patient in patients)
            {
                patientVMs.Add(new PatientReportTableVM() { 
                    OIB = patient.OIB.ToString(),
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    IsolationAddress = patient.IsolationAddress
                });
            }
        }

        private void SetNotFoundStrings(PatientReport report, PatientDashboardVM dashboardVM)
        {
            dashboardVM.NumberOfPeopleInIsolation = report?.NumberOfPatientsInIsolation != null ?
                    report.NumberOfPatientsInIsolation.ToString() : "Podatak nije pronađen.";
            dashboardVM.NumberOfPeopleOutOfIsolation = report?.NumberOfPatientsOutOfIsolation != null ?
                    report.NumberOfPatientsOutOfIsolation.ToString() : "Podatak nije pronađen.";
            dashboardVM.NumberOfPeopleWithSymptoms = report?.NumberOfPatientsWithSymptoms != null ?
                    report.NumberOfPatientsWithSymptoms.ToString() : "Podatak nije pronađen.";
            dashboardVM.NumberOfPeopleOutOfOneKmRadius = report?.NumberOfPatientsThatBrokeIsolation != null ?
                    report.NumberOfPatientsThatBrokeIsolation.ToString() : "Podatak nije pronađen.";

        }
    }
}
