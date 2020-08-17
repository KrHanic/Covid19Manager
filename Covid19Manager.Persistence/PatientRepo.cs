using Covid19Manager.Business.Common;
using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Persistence
{
    public class PatientRepo : IPatientRepo
    {
        public int CreatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetPatientsByStatus(int status)
        {
            throw new NotImplementedException();
        }

        public PatientReport GetPatientsThatBrokeIsolation(PatientFilter filter)
        {
            throw new NotImplementedException();
        }

        public PatientReport GetPatientsWithSymptoms(PatientFilter filter)
        {
            throw new NotImplementedException();
        }

        public void RemovePatientFromIsolation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
