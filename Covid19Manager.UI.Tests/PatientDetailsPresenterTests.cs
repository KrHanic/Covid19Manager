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
            Assert.AreEqual("202008240826", details.LocationTime);
            Assert.AreEqual("36", details.Temperature);
            Assert.AreEqual("Ima", details.Cough);
            Assert.AreEqual("Osjeæa", details.Fatigue);
            Assert.AreEqual("Osjeæa", details.MusclePain);
            Assert.AreEqual("202008240826", details.ConditionTime);
        }

        [TestMethod]
        public void Present_sets_NoDataStringIfNoDataSent()
        {
            PatientDetailsPresenter presenter = new PatientDetailsPresenter();

            PatientDetailsVM details = presenter.Present(new Patient());

            Assert.AreEqual("Podatak nije pronaðen.", details.OIB);
            Assert.AreEqual("Podatak nije pronaðen.", details.Name);
            Assert.AreEqual("Podatak nije pronaðen.", details.IsolationAddress);
            Assert.AreEqual("Podatak nije pronaðen.", details.CurrentLat);
            Assert.AreEqual("Podatak nije pronaðen.", details.CurrentLong);
            Assert.AreEqual("Podatak nije pronaðen.", details.LocationTime);
            Assert.AreEqual("Podatak nije pronaðen.", details.Temperature);
            Assert.AreEqual("Podatak nije pronaðen.", details.Cough);
            Assert.AreEqual("Podatak nije pronaðen.", details.Fatigue);
            Assert.AreEqual("Podatak nije pronaðen.", details.MusclePain);
            Assert.AreEqual("Podatak nije pronaðen.", details.ConditionTime);
        }
    }

}

