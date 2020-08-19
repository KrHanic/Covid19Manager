using Covid19Manager.Business.Common;
using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Persistence
{
    public class PatientRepoDemo : IPatientRepo
    {
        public int CreatePatient(Patient patient)
        {
            return 1;
        }

        public Patient GetPatientDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetPatientsByStatus(int status)
        {
            List<Patient> patients = new List<Patient>();
            for(int i = 1; i < 10; i++)
            {
                patients.Add(new Patient() { 
                    Id = i,
                    OIB = i * 11111111111,
                    FirstName = i.ToString(),
                    LastName = i.ToString() + "ic",
                    IsolationAddress = "majmunova " + i.ToString()
                });
            }
            return patients;
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
