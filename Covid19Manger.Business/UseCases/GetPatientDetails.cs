using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases
{
    public class GetPatientDetails : IGetPatientDetails
    {
        private readonly IPatientRepo _patientRepo;

        public GetPatientDetails(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }
        public Patient Execute(int id)
        {
            return _patientRepo.GetPatientDetails(id);
        }
    }
}
