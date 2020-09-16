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
            PacijentCreateDTO createPatient = new PacijentCreateDTO() { 
                Oib = patient.OIB,
                Ime = patient.FirstName,
                Prezime = patient.LastName,
                AdresaSi = patient.IsolationAddress,
                Lat = Convert.ToDecimal(patient.IsolationLat.Value),
                Long = Convert.ToDecimal(patient.IsolationLong.Value)
            };
            
            var responseContent = CreatePatientAsync(createPatient);
            var id = JsonConvert.DeserializeObject<int>(responseContent.Result);

            return id;
        }


        public Patient GetPatientDetails(int id)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(_baseApiURL + "GetPacijentByID/" + id).Result.Content.ReadAsStringAsync().Result;

            var jsonPatient = JsonConvert.DeserializeObject<PacijentReadDTO>(response);
            
            List<Condition> conditionHistory = new List<Condition>();
            foreach (var condition in jsonPatient.PovijestStanja)
            {
                conditionHistory.Add(new Condition()
                {
                    Temperature = (double)condition.Temperatura,
                    Cough = condition.Kasalj != 0,
                    Fatigue = condition.Umor != 0,
                    MusclePain = condition.BolUMisicima != 0,
                    Time = condition.Vrijeme
                });
            }

            return new Patient()
            {
                Id = (int)jsonPatient.Id,
                OIB = jsonPatient.Oib,
                FirstName = jsonPatient.Ime,
                LastName = jsonPatient.Prezime,
                IsolationAddress = jsonPatient.AdresaSi,
                IsolationLat = (double)jsonPatient.Lat,
                IsolationLong = (double)jsonPatient.Long,
                LastLocation = new Location()
                {
                    PatientCurrentLat = (double)jsonPatient.ZadnjaLokacija.Lat,
                    PatientCurrentLong = (double)jsonPatient.ZadnjaLokacija.Long,
                    Time = jsonPatient.ZadnjaLokacija.Vrijeme
                },
                LastCondition = new Condition()
                {
                    Temperature = (double)jsonPatient.ZadnjeStanje.Temperatura,
                    Cough = jsonPatient.ZadnjeStanje.Kasalj != 0,
                    Fatigue = jsonPatient.ZadnjeStanje.Umor != 0,
                    MusclePain = jsonPatient.ZadnjeStanje.BolUMisicima != 0,
                    Time = jsonPatient.ZadnjeStanje.Vrijeme
                },
                ConditionHistory = conditionHistory
            };
             
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
        public void RemovePatientFromIsolation(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DeleteAsync(_baseApiURL + id).Wait();
        }

        public PatientReport GetPatientsThatBrokeIsolation(PatientFilter filter)
        {
            var responseContent = GetPatientsThatBrokeIsolationAsync(filter);
            IzvjesceDTO izvjesceDTO = JsonConvert.DeserializeObject<IzvjesceDTO>(responseContent.Result);

            List<Patient> patients = new List<Patient>();
            foreach(var patient in izvjesceDTO.Pacijenti)
            {
                patients.Add(new Patient() { 
                    Id = (int)patient.Id,
                    OIB = patient.Oib,
                    FirstName = patient.Ime,
                    LastName = patient.Prezime,
                    IsolationAddress = patient.AdresaSi
                });
            }
            PatientReport report = new PatientReport() { 
                NumberOfPatientsInIsolation = izvjesceDTO.BrojPacijenataUIzolaciji,
                NumberOfPatientsOutOfIsolation = izvjesceDTO.BrojPacijenataVanIzolacije,
                NumberOfPatientsThatBrokeIsolation = izvjesceDTO.BrojPacijenataKojiSuPrekrsiliIzolaciju,
                NumberOfPatientsWithSymptoms = izvjesceDTO.BrojPacijenataSaSimptomima,
                Patients = patients
            };

            return report;
        }

        public PatientReport GetPatientsWithSymptoms(PatientFilter filter)
        {
            var responseContent = GetPatientsWithSymptomsAsync(filter);
            IzvjesceDTO izvjesceDTO = JsonConvert.DeserializeObject<IzvjesceDTO>(responseContent.Result);

            List<Patient> patients = new List<Patient>();
            foreach (var patient in izvjesceDTO.Pacijenti)
            {
                patients.Add(new Patient()
                {
                    Id = (int)patient.Id,
                    OIB = patient.Oib,
                    FirstName = patient.Ime,
                    LastName = patient.Prezime,
                    IsolationAddress = patient.AdresaSi
                });
            }
            PatientReport report = new PatientReport()
            {
                NumberOfPatientsInIsolation = izvjesceDTO.BrojPacijenataUIzolaciji,
                NumberOfPatientsOutOfIsolation = izvjesceDTO.BrojPacijenataVanIzolacije,
                NumberOfPatientsThatBrokeIsolation = izvjesceDTO.BrojPacijenataKojiSuPrekrsiliIzolaciju,
                NumberOfPatientsWithSymptoms = izvjesceDTO.BrojPacijenataSaSimptomima,
                Patients = patients
            };

            return report;
        }


        /////////////////////////////////////////////////////////////////////////////////
        
        private async Task<string> CreatePatientAsync(PacijentCreateDTO createPatient)
        {
            var httpClient = new HttpClient();
            var jsonBody = JsonConvert.SerializeObject(createPatient);
            HttpContent body = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_baseApiURL, body);
            return response.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> GetPatientsThatBrokeIsolationAsync(PatientFilter filter)
        {
            PacijentFilterDTO filterDTO = new PacijentFilterDTO() { 
                VrijemeOd = filter.TimeFromApiFormat,
                VrijemeDo = filter.TimeToApiFormat
            };

            var httpClient = new HttpClient();
            var jsonBody = JsonConvert.SerializeObject(filterDTO);
            HttpContent body = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_baseApiURL + "GetPacijenteKojiSuPrekrsiliIzolaciju/", body);
            return response.Content.ReadAsStringAsync().Result;
        }

        private async Task<string> GetPatientsWithSymptomsAsync(PatientFilter filter)
        {
            PacijentFilterDTO filterDTO = new PacijentFilterDTO()
            {
                VrijemeOd = filter.TimeFromApiFormat,
                VrijemeDo = filter.TimeToApiFormat
            };

            var httpClient = new HttpClient();
            var jsonBody = JsonConvert.SerializeObject(filterDTO);
            HttpContent body = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_baseApiURL + "GetPacijenteKojiImajuSimptome/", body);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
