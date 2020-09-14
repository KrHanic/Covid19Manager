using Covid19Manager.Business.Common;
using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Persistence.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Covid19Manager.Persistence
{
    public class PatientRepository : IPatientRepo
    {
        private readonly string _baseApiURL = "https://localhost:44353/api/pacijenti/";

        public int CreatePatient(Patient patient)
        {
            CreatePatient createPatient = new CreatePatient() { 
                Oib = patient.OIB,
                Ime = patient.FirstName,
                Prezime = patient.LastName,
                AdresaSi = patient.IsolationAddress,
                Lat = Convert.ToDecimal(patient.IsolationLat.Value),
                Long = Convert.ToDecimal(patient.IsolationLong.Value)
            };

            var httpClient = new HttpClient();
            var jsonBody = JsonConvert.SerializeObject(createPatient);
            HttpContent body = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(_baseApiURL + "CreatePacijent", body).Result.Content;

            return 1;
        }

        public Patient GetPatientDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetPatientsByStatus(int status)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(_baseApiURL + "GetPacijentiByStatus/" + status).Result.Content.ReadAsStringAsync().Result;

            List<Patient> patients = new List<Patient>();
            var jsonPatients = JsonConvert.DeserializeObject<List<PacijentReadDTO>>(response);
            foreach (var jsonPatient in jsonPatients)
            {
                List<Condition> conditionHistory = new List<Condition>();
                foreach (var condition in jsonPatient.PovijestStanja)
                {
                    conditionHistory.Add(new Condition() {
                        Temperature = (double)condition.Temperatura,
                        Cough = condition.Kasalj != 0,
                        Fatigue = condition.Umor != 0,
                        MusclePain = condition.BolUMisicima != 0,
                        Time = condition.Vrijeme
                    });
                }

                patients.Add(new Patient() { 
                    Id = (int)jsonPatient.Id,
                    OIB = jsonPatient.Oib,
                    FirstName = jsonPatient.Ime,
                    LastName = jsonPatient.Prezime,
                    IsolationAddress = jsonPatient.AdresaSi,
                    IsolationLat = (double)jsonPatient.Lat,
                    IsolationLong = (double)jsonPatient.Long,
                    LastLocation = new Location() { 
                        PatientCurrentLat = (double)jsonPatient.ZadnjaLokacija.Lat,
                        PatientCurrentLong = (double)jsonPatient.ZadnjaLokacija.Long,
                        Time = jsonPatient.ZadnjaLokacija.Vrijeme
                    },
                    LastCondition = new Condition() { 
                        Temperature = (double)jsonPatient.ZadnjeStanje.Temperatura,
                        Cough = jsonPatient.ZadnjeStanje.Kasalj != 0,
                        Fatigue = jsonPatient.ZadnjeStanje.Umor != 0,
                        MusclePain = jsonPatient.ZadnjeStanje.BolUMisicima != 0,
                        Time = jsonPatient.ZadnjeStanje.Vrijeme
                    },
                    ConditionHistory = conditionHistory
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
