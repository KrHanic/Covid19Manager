using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Entities
{
    public class PatientReport
    {
        public int NumberOfPatientsInIsolation { get; set; }
        public int NumberOfPatientsOutOfIsolation { get; set; }
        public int NumberOfPatientsWithSymptoms { get; set; }
        public int NumberOfPatientsThatBrokeIsolation { get; set; }
        public List<Patient> Patients { get; set; }
    }
}
