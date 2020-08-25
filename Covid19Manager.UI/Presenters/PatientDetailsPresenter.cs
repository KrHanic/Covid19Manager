using Covid19Manager.Business.Entities;
using Covid19Manager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.Presenters
{
    public class PatientDetailsPresenter
    {
        public PatientDetailsVM Present(Patient patient)
        {
            PatientDetailsVM detailsVM = new PatientDetailsVM() {
                ConditionHistory = new List<PatientDetailsConditionVM>()
            };

            ConvertConditionHistory(patient, detailsVM);
            SetConditionBoolsToStrings(patient, detailsVM);
            FormatTemperatureToOneDecimalPlace(patient, detailsVM);
            SetNotFoundStrings(patient, detailsVM);
            FormatDateTimeStrings(detailsVM);
            FormatConditionHistoryDateTime(detailsVM);
            FormatConditionHistoryTemperatureToOneDecimalPlace(patient, detailsVM);

            return detailsVM;   
        }

        private void FormatConditionHistoryTemperatureToOneDecimalPlace(Patient patient, PatientDetailsVM detailsVM)
        {
            if (patient?.ConditionHistory != null)
            {
                for (int i = 0; i < patient.ConditionHistory.Count(); i++)
                {
                    detailsVM.ConditionHistory[i].Temperature = patient.ConditionHistory[i].Temperature.Value.ToString("0.0");
                }
            }
        }

        private void FormatTemperatureToOneDecimalPlace(Patient patient, PatientDetailsVM detailsVM)
        {
            if (patient?.LastCondition?.Temperature != null)
                detailsVM.Temperature = patient?.LastCondition?.Temperature.Value.ToString("0.0"); 
        }

        private void FormatConditionHistoryDateTime(PatientDetailsVM detailsVM)
        {
            if (detailsVM?.ConditionHistory != null)
            {
                foreach (var condition in detailsVM.ConditionHistory)
                {
                    condition.Time = DateTime.ParseExact(
                            condition.Time,
                            "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                            .ToString("dd.MM.yyyy HH:mm");
                }
            }
        }

        private void FormatDateTimeStrings(PatientDetailsVM detailsVM)
        {
            if(detailsVM?.ConditionTime != null && detailsVM?.ConditionTime != "Podatak nije pronađen.")
                detailsVM.ConditionTime = DateTime.ParseExact(
                            detailsVM.ConditionTime,
                            "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                            .ToString("dd.MM.yyyy HH:mm");
            if (detailsVM?.LocationTime != null && detailsVM?.LocationTime != "Podatak nije pronađen.")
                detailsVM.LocationTime = DateTime.ParseExact(
                            detailsVM.LocationTime,
                            "yyyyMMddHHmm", CultureInfo.InvariantCulture)
                            .ToString("dd.MM.yyyy HH:mm");
        }

        private void ConvertConditionHistory(Patient patient, PatientDetailsVM detailsVM)
        {
            if (patient?.ConditionHistory != null)
            {
                foreach (var condition in patient.ConditionHistory)
                {
                    detailsVM.ConditionHistory.Add(new PatientDetailsConditionVM()
                    {
                        Temperature = condition.Temperature.ToString(),
                        Time = condition.Time.ToString()
                    });
                }
            }
        }

        private void SetNotFoundStrings(Patient patient, PatientDetailsVM detailsVM)
        { 
            detailsVM.OIB = patient?.OIB.ToString().Length == 11 ?
                    patient.OIB.ToString() : "Podatak nije pronađen.";
            detailsVM.Name = patient?.LastName != null && patient?.FirstName != null ?
                    $"{patient.LastName} {patient.FirstName}" : "Podatak nije pronađen.";
            detailsVM.IsolationAddress = patient?.IsolationAddress != null ?
                    patient.IsolationAddress.ToString() : "Podatak nije pronađen.";
            detailsVM.CurrentLat = patient?.LastLocation?.PatientCurrentLat != null ?
                    patient.LastLocation.PatientCurrentLat.ToString() : "Podatak nije pronađen.";
            detailsVM.CurrentLong = patient?.LastLocation?.PatientCurrentLong != null ?
                    patient.LastLocation.PatientCurrentLong.ToString() : "Podatak nije pronađen.";
            detailsVM.LocationTime = patient?.LastLocation?.Time != null ?
                    patient.LastLocation.Time.ToString() : "Podatak nije pronađen.";
            detailsVM.Temperature = patient?.LastCondition?.Temperature != null ?
                    detailsVM.Temperature : "Podatak nije pronađen.";
            detailsVM.Cough = patient?.LastCondition?.Cough != null ?
                    detailsVM.Cough : "Podatak nije pronađen.";
            detailsVM.Fatigue = patient?.LastCondition?.Fatigue != null ?
                    detailsVM.Fatigue : "Podatak nije pronađen.";
            detailsVM.MusclePain = patient?.LastCondition?.MusclePain != null ?
                    detailsVM.MusclePain : "Podatak nije pronađen.";
            detailsVM.ConditionTime = patient?.LastCondition?.Time != null ?
                    patient.LastCondition.Time.ToString() : "Podatak nije pronađen.";
        }

        private void SetConditionBoolsToStrings(Patient patient, PatientDetailsVM detailsVM)
        {
            detailsVM.Cough = patient?.LastCondition?.Cough.Value ?? false
                                && patient.LastCondition.Cough.Value 
                                ? "Ima" : "Nema";
            detailsVM.Fatigue = patient?.LastCondition?.Fatigue ?? false 
                                && patient.LastCondition.Fatigue.Value
                                ? "Osjeća" : "Ne osjeća";
            detailsVM.MusclePain = patient?.LastCondition?.MusclePain ?? false 
                                && patient.LastCondition.MusclePain.Value
                                ? "Osjeća" : "Ne osjeća";
        }
    }
}
