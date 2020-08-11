using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases
{
    public class GetPatients
    {
        private readonly IPatientRepo _patientRepo;

        public GetPatients(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }
        public List<Patient> ByStatus(int status)
        {
            return _patientRepo.GetPatientsByStatus(status);
        }
    }
}
