﻿using Covid19Manager.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Repositories
{
    public interface IPatientRepo
    {
        public List<Patient> GetPatientsByStatus(int status);
        public Patient GetPatientDetails(int id);
        public void RemovePatientFromIsolation(int id);
        public int CreatePatient(Patient patient);
    }
}