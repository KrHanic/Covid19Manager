using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases
{
    public class GetPatients : IGetPatients
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
