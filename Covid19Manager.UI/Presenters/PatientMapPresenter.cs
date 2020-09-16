using Covid19Manager.Business.Entities;
using Covid19Manager.UI.ViewModels;
using Geolocation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.Presenters
{
    public class PatientMapPresenter
    {
        public List<PatientMapVM> Present(List<Patient> patients)
        {
            List<PatientMapVM> patientVMs = new List<PatientMapVM>();
            foreach (var patient in patients)
            {
                PatientMapVM patientMapVM = new PatientMapVM();

                SetConditionBoolsToStrings(patient, patientMapVM);
                FormatTemperatureToOneDecimalPlace(patient, patientMapVM);
                SetNotFoundStrings(patient, patientMapVM);
                FormatDateTimeStrings(patientMapVM);
                if (patientIsOutOfOneKmRadius(patient))
                    setPatientColorOrange(patientMapVM);
                else if (patientTempIsTooHigh(patient))
                    setPatientColorRed(patientMapVM);
                else
                    setPatientColorGreen(patientMapVM);

                patientVMs.Add(patientMapVM);
            }
            return patientVMs;
        }

        private void setPatientColorGreen(PatientMapVM patientMapVM)
        {
            patientMapVM.MapMarkerColor = "green";
        }

        private void setPatientColorRed(PatientMapVM patientMapVM)
        {
            patientMapVM.MapMarkerColor = "red";
        }

        private void setPatientColorOrange(PatientMapVM patientMapVM)
        {
            patientMapVM.MapMarkerColor = "orange";
        }

        private bool patientTempIsTooHigh(Patient patient)
        {
            if (!patient?.LastCondition?.Temperature.HasValue ?? true)
                return false;
            return patient.LastCondition.Temperature >= 37;
        }

        private bool patientIsOutOfOneKmRadius(Patient patient)
        {
            if (!patient.IsolationLat.HasValue
                || !patient.IsolationLong.HasValue
                || !patient.LastLocation.PatientCurrentLat.HasValue
                || !patient.LastLocation.PatientCurrentLat.HasValue)
                return false;

            Coordinate IsolationCoordinates = 
                new Coordinate(patient.IsolationLat.Value, patient.IsolationLong.Value);
            Coordinate CurrentCoordinates = 
                new Coordinate(patient.LastLocation.PatientCurrentLat.Value, patient.LastLocation.PatientCurrentLong.Value);

            double distanceInMiles = GeoCalculator.GetDistance(IsolationCoordinates, CurrentCoordinates, 6);
            double distanceInKm = distanceInMiles * 1.609344;

            return distanceInKm > 1;
        }

        private void FormatDateTimeStrings(PatientMapVM patientMapVM)
        {
            if (patientMapVM?.ConditionTime != null && patientMapVM?.ConditionTime != "Podatak nije pronađen.")
                patientMapVM.ConditionTime = DateTime.ParseExact(
                            patientMapVM.ConditionTime,
                            "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                            .ToString("dd.MM.yyyy. HH:mm");
            if (patientMapVM?.LocationTime != null && patientMapVM?.LocationTime != "Podatak nije pronađen.")
                patientMapVM.LocationTime = DateTime.ParseExact(
                            patientMapVM.LocationTime,
                            "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                            .ToString("dd.MM.yyyy. HH:mm");
        }

        private void FormatTemperatureToOneDecimalPlace(Patient patient, PatientMapVM patientMapVM)
        {
            if (patient?.LastCondition?.Temperature != null)
                patientMapVM.Temperature = patient?.LastCondition?.Temperature.Value.ToString("0.0");
        }

        private void SetConditionBoolsToStrings(Patient patient, PatientMapVM patientMapVM)
        {
            patientMapVM.Cough = patient?.LastCondition?.Cough.HasValue ?? false
                                && patient.LastCondition.Cough.Value
                                ? "Ima" : "Nema";
            patientMapVM.Fatigue = patient?.LastCondition?.Fatigue ?? false
                                && patient.LastCondition.Fatigue.Value
                                ? "Osjeća" : "Ne osjeća";
            patientMapVM.MusclePain = patient?.LastCondition?.MusclePain ?? false
                                && patient.LastCondition.MusclePain.Value
                                ? "Osjeća" : "Ne osjeća";
        }

        private void SetNotFoundStrings(Patient patient, PatientMapVM patientMapVM)
        {
            patientMapVM.Name = patient?.LastName != null && patient?.FirstName != null ?
                    $"{patient.LastName} {patient.FirstName}" : "Podatak nije pronađen.";
            patientMapVM.CurrentLat = patient?.LastLocation?.PatientCurrentLat.Value != null ?
                    patient.LastLocation.PatientCurrentLat.Value.ToString() : "Podatak nije pronađen.";
            patientMapVM.CurrentLong = patient?.LastLocation?.PatientCurrentLong.Value != null ?
                    patient.LastLocation.PatientCurrentLong.Value.ToString() : "Podatak nije pronađen.";
            patientMapVM.LocationTime = patient?.LastLocation?.Time != null && patient?.LastLocation?.Time > 100000000000 ?
                    patient.LastLocation.Time.ToString() : "Podatak nije pronađen.";
            patientMapVM.Temperature = patient?.LastCondition?.Temperature != null && patient?.LastCondition?.Temperature > 30 ?
                    patientMapVM.Temperature : "Podatak nije pronađen.";
            patientMapVM.Cough = patient?.LastCondition?.Cough != null ?
                    patientMapVM.Cough : "Podatak nije pronađen.";
            patientMapVM.Fatigue = patient?.LastCondition?.Fatigue != null ?
                    patientMapVM.Fatigue : "Podatak nije pronađen.";
            patientMapVM.MusclePain = patient?.LastCondition?.MusclePain != null ?
                    patientMapVM.MusclePain : "Podatak nije pronađen.";
            patientMapVM.ConditionTime = patient?.LastCondition?.Time != null && patient?.LastCondition?.Time > 100000000000 ?
                    patient.LastCondition.Time.ToString() : "Podatak nije pronađen.";
        }
    }
}
