using Covid19Manager.Business.Entities;
using Covid19Manager.UI.Presenters;
using Covid19Manager.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.UI.Tests
{
    [TestClass]
    public class RemovePatientFromIsolationPresenterTests
    {
        private Patient patient;

        [TestInitialize]
        public void SetUp()
        {
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
                    PatientCurrentLong = 12.12,
                    Time = 202008240826
                },
                LastCondition = new Condition()
                {
                    Temperature = 36,
                    Cough = true,
                    Fatigue = true,
                    MusclePain = true,
                    Time = 202008240826
                },
                ConditionHistory = new List<Condition>()
                {
                    new Condition()
                    {
                        Temperature = 36,
                        Time = 202008240826
                    },
                    new Condition()
                    {
                        Temperature = 37,
                        Time = 202008230826
                    },
                    new Condition()
                    {
                        Temperature = 38,
                        Time = 202008220826
                    }
                }
            };
        }

        [TestMethod]
        public void Present_returns_CorrectVmData()
        {
            RemovePatientFromIsolationPresenter presenter = new RemovePatientFromIsolationPresenter();

            RemovePatientFromIsolationVM patientInfo = presenter.Present(patient);

            Assert.AreEqual("1", patientInfo.Patient_id);
            Assert.AreEqual("11111111111", patientInfo.OIB);
            Assert.AreEqual("prezime ime", patientInfo.Name);
            Assert.AreEqual("majmunova 1", patientInfo.IsolationaAddress);
        }
    }
}
