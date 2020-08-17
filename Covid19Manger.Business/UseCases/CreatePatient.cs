using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases
{
    public class CreatePatient : ICreatePatient
    {
        private IPatientRepo _patientRepo;

        public CreatePatient(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public int Execute(Patient patient)
        {
            SetPatientStatusToUnregistered(patient);
            VerifyPatient(patient);
            
            return _patientRepo.CreatePatient(patient);
        }

        private void VerifyPatient(Patient patient)
        {
            VerifyFirstName(patient);
            VerifyLastName(patient);
            VerifyIsolationAddress(patient);
            VerifyOib(patient);
            VerifyLat(patient);
            VerifyLong(patient);
        }

        private void VerifyLong(Patient patient)
        {
            if (patient.IsolationLong < -180 || patient.IsolationLong > 180 || patient.IsolationLong == null)
                throw new ArgumentException("Polje longituda nije popunjejno ili longituda nije ispravana.");
        }

        private void VerifyLat(Patient patient)
        {
            if (patient.IsolationLat < -90 || patient.IsolationLat > 90 || patient.IsolationLat == null)
                throw new ArgumentException("Polje latituda nije popunjejno ili latituda nije ispravana.");
        }

        private void VerifyOib(Patient patient)
        {
            if(patient.OIB.ToString().Length != 11)
                throw new ArgumentException("Polje oib nije popunjejno ili oib nije ispravan.");
        }

        private void VerifyIsolationAddress(Patient patient)
        {
            if (String.IsNullOrEmpty(patient.IsolationAddress?.Trim()))
                throw new ArgumentException("Polje adresa samoizolacije nije popunjejno.");
        }

        private void VerifyLastName(Patient patient)
        {
            if (String.IsNullOrEmpty(patient.LastName?.Trim()))
                throw new ArgumentException("Polje prezime nije popunjejno.");
        }

        private void VerifyFirstName(Patient patient)
        {
            if (String.IsNullOrEmpty(patient.FirstName?.Trim()))
                throw new ArgumentException("Polje ime nije popunjejno.");
        }

        private void SetPatientStatusToUnregistered(Patient patient)
        {
            patient.StatusId = 1;
        }
    }
}
