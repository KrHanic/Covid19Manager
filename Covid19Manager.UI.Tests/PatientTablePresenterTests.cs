using Covid19Manager.Business.Entities;
using Covid19Manager.Business.UseCases.Interfaces;
using Covid19Manager.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Covid19Manager.UI.Tests
{
    [TestClass]
    public class PatientTableScreenTests
    {
        private List<Patient> patients;
        private Patient patient;
        [TestInitialize]
        public void SetUp()
        {
            for (int i = 1; i < 10; i++)
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
                    LastLocation = new Location() {
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
            patient = new Patient()
            {
                Id = 1,
                OIB = 11111111111,
                FirstName = "ime",
                LastName = "prezime",
                IsolationAddress = "majmunova 1",
                IsolationLat = 12.12,
                IsolationLong = 12.12,
                LastLocation = new Location()
                {
                    PatientCurrentLat = 12.12,
                    PatientCurrentLong = 12.12
                },
                LastCondition = new Condition()
                {
                    Temperature = 36
                }
            };
        }

        [TestMethod]
        public void nothing()
        {

        }
    }
}
