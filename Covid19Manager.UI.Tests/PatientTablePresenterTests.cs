using Covid19Manager.Business.Entities;
using Covid19Manager.Business.UseCases.Interfaces;
using Covid19Manager.UI.Presenters;
using Covid19Manager.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Covid19Manager.UI.Tests
{
    [TestClass]
    public class PatientTableScreenTests
    {
        private List<Patient> patients = new List<Patient>();
        private Patient patient;
        private List<Patient> onePatientList = new List<Patient>();

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
            
            onePatientList.Add(patient);
        }

        [TestMethod]
        public void PatientTablePresenter_SetsRowColor_OrangeIfLocationIsOutOfOneKmRadius()
        {
            //added values result in distance of 1.02Km
            onePatientList[0].LastLocation.PatientCurrentLat = 12.12 + 0.007;
            onePatientList[0].LastLocation.PatientCurrentLong = 12.12 + 0.006;

            PatientTablePresenter presenter = new PatientTablePresenter();
            List<PatientTableVM> patientsVM = presenter.SetRowColors(onePatientList);

            Assert.AreEqual("table-warning", patientsVM[0].RowColor);
        }

        [TestMethod]
        public void PatientTablePresenter_SetsRowColor_RedIfTempIs37OrHigher()
        {
            onePatientList[0].LastCondition.Temperature = 37;

            PatientTablePresenter presenter = new PatientTablePresenter();
            List<PatientTableVM> patientsVM = presenter.SetRowColors(onePatientList);

            Assert.AreEqual("table-danger", patientsVM[0].RowColor);
        }

        [TestMethod]
        public void PatientTablePresenter_SetsRowColor_GreenIfPatientTempAndLocationIsOk()
        {
            PatientTablePresenter presenter = new PatientTablePresenter();
            List<PatientTableVM> patientsVM = presenter.SetRowColors(onePatientList);

            Assert.AreEqual("table-success", patientsVM[0].RowColor);
        }

        [TestMethod]
        public void PatientTablePresenter_SetsRowColor_ToCorrectColorForListOfPatients()
        {
            PatientTablePresenter presenter = new PatientTablePresenter();
            List<PatientTableVM> patientsVM = presenter.SetRowColors(patients);

            Assert.AreEqual("table-danger", patientsVM[0].RowColor);
            Assert.AreEqual("table-danger", patientsVM[1].RowColor);
            Assert.AreEqual("table-success", patientsVM[2].RowColor);
            Assert.AreEqual("table-success", patientsVM[3].RowColor);
            Assert.AreEqual("table-success", patientsVM[4].RowColor);
            Assert.AreEqual("table-success", patientsVM[5].RowColor);
            Assert.AreEqual("table-success", patientsVM[6].RowColor);
            Assert.AreEqual("table-success", patientsVM[7].RowColor);
            Assert.AreEqual("table-warning", patientsVM[8].RowColor);
        }
    }
}
