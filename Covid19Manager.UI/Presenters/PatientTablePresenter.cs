using Covid19Manager.Business.Entities;
using Covid19Manager.UI.ViewModels;
using Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.Presenters
{
    public class PatientTablePresenter
    {
        public List<PatientTableVM> SetRowColors(List<Patient> patients)
        {
            List<PatientTableVM> patientsVM = new List<PatientTableVM>();

            foreach(var patient in patients)
            {
                var patientVM = new PatientTableVM() {
                    ID = patient.Id,
                    OIB = patient.OIB,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    IsolationAddress = patient.IsolationAddress,
                    RowColor = ""
                };

                if (patientIsOutOfOneKmRadius(patient))
                    setPatientColorOrange(patientVM);
                else if (patientTempIsTooHigh(patient))
                    setPatientColorRed(patientVM);
                else
                    setPatientColorGreen(patientVM);

                patientsVM.Add(patientVM);
            }

            return patientsVM;
        }


        private bool patientTempIsTooHigh(Patient patient)
        {
            return patient.LastCondition.Temperature >= 37;
        }

        private bool patientIsOutOfOneKmRadius(Patient patient)
        {
            Coordinate IsolationCoordinates = new Coordinate(patient.IsolationLat.Value, patient.IsolationLong.Value);
            Coordinate CurrentCoordinates = new Coordinate(patient.LastLocation.PatientCurrentLat.Value, patient.LastLocation.PatientCurrentLong.Value);

            double distanceInMiles = GeoCalculator.GetDistance(IsolationCoordinates, CurrentCoordinates, 6);
            double distanceInKm = distanceInMiles * 1.609344;

            return distanceInKm > 1;
        }
        private void setPatientColorGreen(PatientTableVM patientVM)
        {
            patientVM.RowColor = "table-success";
        }

        private void setPatientColorRed(PatientTableVM patientVM)
        {
            patientVM.RowColor = "table-danger";
        }
        private void setPatientColorOrange(PatientTableVM patientVM)
        {
            patientVM.RowColor = "table-warning";
        }
    }
}
