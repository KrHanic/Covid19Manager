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
    public class PatientMapPresenterTests
    {
        private List<Patient> patients = new List<Patient>();

        [TestInitialize]
        public void SetUp()
        {
            for (int i = 1; i < 3; i++)
            {
                patients.Add(new Patient()
                {
                    FirstName = "ime",
                    LastName = "prezime",
                    IsolationLat = 12.12,
                    IsolationLong = 12.12,
                    LastLocation = new Location()
                    {
                        PatientCurrentLat = 12.12,
                        PatientCurrentLong = 12.12,
                        Time = 202008251335
                    },
                    LastCondition = new Condition()
                    {
                        Temperature = 36,
                        Cough = true,
                        Fatigue = true,
                        MusclePain = true,
                        Time = 202008251335
                    }
                });
            }
        }

        [TestMethod]
        public void Present_setsAndFormats_dataToMapVm()
        {
            PatientMapPresenter presenter = new PatientMapPresenter();

            List<PatientMapVM> patientVMs = presenter.Present(patients);

            Assert.AreEqual("prezime ime", patientVMs[0].Name);
            Assert.AreEqual("12,12", patientVMs[0].CurrentLat);
            Assert.AreEqual("12,12", patientVMs[0].CurrentLong);
            Assert.AreEqual("25.08.2020. 13:35", patientVMs[0].LocationTime);
            Assert.AreEqual("36,0", patientVMs[0].Temperature);
            Assert.AreEqual("Ima", patientVMs[0].Cough);
            Assert.AreEqual("Osjeća", patientVMs[0].Fatigue);
            Assert.AreEqual("Osjeća", patientVMs[0].MusclePain);
            Assert.AreEqual("25.08.2020. 13:35", patientVMs[0].ConditionTime);
        }

        [TestMethod]
        public void Present_sets_NoDataStrings()
        {
            PatientMapPresenter presenter = new PatientMapPresenter();

            List<PatientMapVM> patientVMs = presenter.Present(new List<Patient>() { 
                new Patient()
            });

            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].Name);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].CurrentLat);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].CurrentLong);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].LocationTime);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].Temperature);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].Cough);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].Fatigue);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].MusclePain);
            Assert.AreEqual("Podatak nije pronađen.", patientVMs[0].ConditionTime);
        }

        [TestMethod]
        public void Present_setsMarkerColor_OrangeIfLocationIsOutOfOneKmRadius()
        {
            PatientMapPresenter presenter = new PatientMapPresenter();
            //added values result in distance of 1.02Km
            patients[0].LastLocation.PatientCurrentLat += 0.007;
            patients[0].LastLocation.PatientCurrentLong += 0.006;

            List<PatientMapVM> patientVMs = presenter.Present(patients);

            Assert.AreEqual("orange", patientVMs[0].MapMarkerColor);
        }

        [TestMethod]
        public void Present_setsMarkerColor_RedIfTempIs37OrHigher()
        {
            PatientMapPresenter presenter = new PatientMapPresenter();
            patients[0].LastCondition.Temperature = 37;

            List<PatientMapVM> patientVMs = presenter.Present(patients);

            Assert.AreEqual("red", patientVMs[0].MapMarkerColor);
        }

        [TestMethod]
        public void Present_setsMarkerColor_GreenIfPatientTempAndLocationIsOk()
        {
            PatientMapPresenter presenter = new PatientMapPresenter();

            List<PatientMapVM> patientVMs = presenter.Present(patients);

            Assert.AreEqual("green", patientVMs[0].MapMarkerColor);
        }
    }
}
