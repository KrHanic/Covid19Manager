using Covid19Manager.Business.Entities;
using Covid19Manager.UI.Enums;
using Covid19Manager.UI.Presenters;
using Covid19Manager.UI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Covid19Manager.UI.Tests
{
    [TestClass]
    public class PatientDashboardTests
    {
        private PatientReport report = new PatientReport();

        [TestInitialize]
        public void SetUp()
        {
            report.NumberOfPatientsInIsolation = 10;
            report.NumberOfPatientsOutOfIsolation = 9;
            report.NumberOfPatientsThatBrokeIsolation = 8;
            report.NumberOfPatientsWithSymptoms = 7;
            report.Patients = new List<Patient>() { 
                new Patient()
                {
                    OIB = 11111111111,
                    FirstName = "Ime",
                    LastName = "Prezime",
                    IsolationAddress = "Majmunova 1"
                },
                new Patient()
                {
                    OIB = 11111111112,
                    FirstName = "Ime2",
                    LastName = "Prezime2",
                    IsolationAddress = "Majmunova 2"
                }
            };
        }

        [TestMethod]
        public void Presenter_sets_notFoundStrings()
        {
            PatientDashboardPresenter presenter = new PatientDashboardPresenter(new PatientDashboardVM());

            PatientDashboardVM reportVM = presenter.Present(new PatientReport() { 
                Patients = new List<Patient>()
            });

            Assert.AreEqual("Podatak nije pronađen.", reportVM.NumberOfPeopleInIsolation);
            Assert.AreEqual("Podatak nije pronađen.", reportVM.NumberOfPeopleOutOfIsolation);
            Assert.AreEqual("Podatak nije pronađen.", reportVM.NumberOfPeopleOutOfOneKmRadius);
            Assert.AreEqual("Podatak nije pronađen.", reportVM.NumberOfPeopleWithSymptoms);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void Presenter_sets_AllData(int PatientReportListId)
        {
            PatientDashboardVM dashboardVM = new PatientDashboardVM();
            if (PatientReportListId == (int)PatientReportTables.PatientsWithSymptoms)
            {
                dashboardVM.IsJustCreated = false;
                dashboardVM.ReportTableId = (int)PatientReportTables.PatientsWithSymptoms;
            }
            PatientDashboardPresenter presenter = new PatientDashboardPresenter(dashboardVM);

            PatientDashboardVM reportVM = presenter.Present(report);

            Assert.AreEqual("10", reportVM.NumberOfPeopleInIsolation);
            Assert.AreEqual("9", reportVM.NumberOfPeopleOutOfIsolation);
            Assert.AreEqual("8", reportVM.NumberOfPeopleOutOfOneKmRadius);
            Assert.AreEqual("7", reportVM.NumberOfPeopleWithSymptoms);
            Assert.AreEqual(false, reportVM.IsJustCreated);

            if (PatientReportListId == (int)PatientReportTables.PatientsWithSymptoms)
            {
                Assert.AreEqual("11111111111", reportVM.PatientsWithSymptoms[0].OIB);
                Assert.AreEqual("Ime", reportVM.PatientsWithSymptoms[0].FirstName);
                Assert.AreEqual("Prezime", reportVM.PatientsWithSymptoms[0].LastName);
                Assert.AreEqual("Majmunova 1", reportVM.PatientsWithSymptoms[0].IsolationAddress);
            }
            else
            {
                Assert.AreEqual("11111111112", reportVM.PatientsOutOfOneKmRadius[1].OIB);
                Assert.AreEqual("Ime2", reportVM.PatientsOutOfOneKmRadius[1].FirstName);
                Assert.AreEqual("Prezime2", reportVM.PatientsOutOfOneKmRadius[1].LastName);
                Assert.AreEqual("Majmunova 2", reportVM.PatientsOutOfOneKmRadius[1].IsolationAddress);
            }


        }
    }
}
