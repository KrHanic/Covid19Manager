using Covid19Manager.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases
{
    public class RemovePatientFromIsolation
    {
        private readonly IPatientRepo _patientRepo;

        public RemovePatientFromIsolation(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }
        public void Execute(int id)
        {
            _patientRepo.RemovePatientFromIsolation(id);
        }
    }
}
