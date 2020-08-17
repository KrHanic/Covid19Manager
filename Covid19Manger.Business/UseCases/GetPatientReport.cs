using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases
{
    public class GetPatientReport
    {
        private IPatientRepo _patientRepo;

        public GetPatientReport(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public PatientReport GetPatientsWithSymptoms()
        {
            return _patientRepo.GetPatientsWithSymptoms();
        }

        public PatientReport GetPatientsThatBrokeIsolation()
        {
            return _patientRepo.GetPatientsThatBrokeIsolation();
        }
    }
}
