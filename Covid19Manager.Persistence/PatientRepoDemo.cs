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
            return new Patient()
            {
                Id = 1,
                OIB = 11111111111,
                FirstName = "ime",
                LastName = "prezime",
                IsolationAddress = "majmunova 3",
                IsolationLat = 12.12,
                IsolationLong = 12.12,
                LastLocation = new Location()
                {
                    PatientCurrentLat = 12.12,
                    PatientCurrentLong = 12.12,
                    Time = 202008191444
                },
                LastCondition = new Condition()
                {
                    Temperature = 36,
                    Cough = true,
                    Fatigue = true,
                    MusclePain = true,
                    Time = 202008211405
                },
                ConditionHistory = new List<Condition>()
                {
                    new Condition()
                    {
                        Temperature = 36,
                        Cough = true,
                        Fatigue = true,
                        MusclePain = true,
                        Time = 202008211405
                    },
                    new Condition()
                    {
                        Temperature = 36.2,
                        Time = 202008201407
                    },
                    new Condition()
                    {
                        Temperature = 36.3,
                        Time = 202008191407
                    }
                }
            };
        }

        public List<Patient> GetPatientsByStatus(int status)
        {
            List<Patient> patients = new List<Patient>();
            for (int i = 1; i < 11; i++)
            {
                patients.Add(new Patient()
                {
                    Id = i,
                    OIB = i * 11111111111,
                    FirstName = i.ToString(),
                    LastName = i.ToString() + "ic",
                    IsolationAddress = "majmunova " + i.ToString(),
                    IsolationLat = 12.12,
                    IsolationLong = 12.12,
                    LastLocation = new Location()
                    {
                        PatientCurrentLat = 12.12 + i * 0.001,
                        PatientCurrentLong = 12.12,
                        Time = 202008191444
                    },
                    LastCondition = new Condition()
                    {
                        Temperature = 38 - i * 0.40
                    }
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
