using Covid19Manager.Business.Common;
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

        public PatientReport GetPatientsWithSymptoms(PatientFilter filter)
        {
            return _patientRepo.GetPatientsWithSymptoms(filter);
        }

        public PatientReport GetPatientsThatBrokeIsolation(PatientFilter filter)
        {
            return _patientRepo.GetPatientsThatBrokeIsolation(filter);
        }
    }
}
