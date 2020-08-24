using Covid19Manager.Business.Entities;
using Covid19Manager.Business.UseCases.Interfaces;
using Covid19Manager.UI.Presenters;
using Covid19Manager.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Covid19Manager.UI.Tests
{
    [TestClass]
    public class PatientDetailsScreenTests
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
        public void Present_converts_PatientToPatientDetailsVM()
        {
            PatientDetailsPresenter presenter = new PatientDetailsPresenter();

            PatientDetailsVM details = presenter.Present(patient);

            Assert.AreEqual("11111111111", details.OIB);
            Assert.AreEqual("prezime ime", details.Name);
            Assert.AreEqual("majmunova 1", details.IsolationAddress);
            Assert.AreEqual("12,12", details.CurrentLat);
            Assert.AreEqual("12,12", details.CurrentLong);
            Assert.AreEqual("24.08.2020 08:26", details.LocationTime);
            Assert.AreEqual("36,0", details.Temperature);
            Assert.AreEqual("Ima", details.Cough);
            Assert.AreEqual("Osjeća", details.Fatigue);
            Assert.AreEqual("Osjeća", details.MusclePain);
            Assert.AreEqual("24.08.2020 08:26", details.ConditionTime);
            Assert.AreEqual("24.08.2020 08:26", details.ConditionHistory[0].Time);
            Assert.AreEqual("23.08.2020 08:26", details.ConditionHistory[1].Time);
            Assert.AreEqual("22.08.2020 08:26", details.ConditionHistory[2].Time);
            Assert.AreEqual("36,0", details.ConditionHistory[0].Temperature);
        }

        [TestMethod]
        public void Present_sets_NoDataStringIfNoDataSent()
        {
            PatientDetailsPresenter presenter = new PatientDetailsPresenter();

            PatientDetailsVM details = presenter.Present(new Patient());

            Assert.AreEqual("Podatak nije pronađen.", details.OIB);
            Assert.AreEqual("Podatak nije pronađen.", details.Name);
            Assert.AreEqual("Podatak nije pronađen.", details.IsolationAddress);
            Assert.AreEqual("Podatak nije pronađen.", details.CurrentLat);
            Assert.AreEqual("Podatak nije pronađen.", details.CurrentLong);
            Assert.AreEqual("Podatak nije pronađen.", details.LocationTime);
            Assert.AreEqual("Podatak nije pronađen.", details.Temperature);
            Assert.AreEqual("Podatak nije pronađen.", details.Cough);
            Assert.AreEqual("Podatak nije pronađen.", details.Fatigue);
            Assert.AreEqual("Podatak nije pronađen.", details.MusclePain);
            Assert.AreEqual("Podatak nije pronađen.", details.ConditionTime);
        }
    }

}

