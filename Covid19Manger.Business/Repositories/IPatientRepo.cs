using Covid19Manager.Business.Common;
using Covid19Manager.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Covid19Manager.Business.Repositories
{
    public interface IPatientRepo
    {
        public List<Patient> GetPatientsByStatus(int status);
        public Patient GetPatientDetails(int id);
        public void RemovePatientFromIsolation(int id);
        public int CreatePatient(Patient patient);
        public PatientReport GetPatientsWithSymptoms(PatientFilter filter);
        public PatientReport GetPatientsThatBrokeIsolation(PatientFilter filter);
    }
}
