using Covid19Manager.Business.Entities;
using Covid19Manager.UI.ViewModels;
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

                patientVMs.Add(patientMapVM);
            }
            return patientVMs;
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
                patientMapVM.Temperature = patient?.LastCondition?.Temperature.ToString("0.0");
        }

        private void SetConditionBoolsToStrings(Patient patient, PatientMapVM patientMapVM)
        {
            patientMapVM.Cough = patient?.LastCondition?.Cough.Value ?? false
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
            patientMapVM.CurrentLat = patient?.LastLocation?.PatientCurrentLat != null ?
                    patient.LastLocation.PatientCurrentLat.ToString() : "Podatak nije pronađen.";
            patientMapVM.CurrentLong = patient?.LastLocation?.PatientCurrentLong != null ?
                    patient.LastLocation.PatientCurrentLong.ToString() : "Podatak nije pronađen.";
            patientMapVM.LocationTime = patient?.LastLocation?.Time != null ?
                    patient.LastLocation.Time.ToString() : "Podatak nije pronađen.";
            patientMapVM.Temperature = patient?.LastCondition?.Temperature != null ?
                    patientMapVM.Temperature : "Podatak nije pronađen.";
            patientMapVM.Cough = patient?.LastCondition?.Cough != null ?
                    patientMapVM.Cough : "Podatak nije pronađen.";
            patientMapVM.Fatigue = patient?.LastCondition?.Fatigue != null ?
                    patientMapVM.Fatigue : "Podatak nije pronađen.";
            patientMapVM.MusclePain = patient?.LastCondition?.MusclePain != null ?
                    patientMapVM.MusclePain : "Podatak nije pronađen.";
            patientMapVM.ConditionTime = patient?.LastCondition?.Time != null ?
                    patient.LastCondition.Time.ToString() : "Podatak nije pronađen.";
        }
    }
}
