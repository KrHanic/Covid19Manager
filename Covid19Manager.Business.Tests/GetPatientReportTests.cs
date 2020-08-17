using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Tests
{
    [TestClass]
    public class GetPatientReportTests
    {
        [TestMethod]
        public void GetPatientsWithSymptoms_calls_CorrectRepoMethod()
        {
            Mock<IPatientRepo> _patientRepoMock = new Mock<IPatientRepo>();
            _patientRepoMock.Setup(m => m.GetPatientsWithSymptoms()).Returns(new PatientReport());
            GetPatientReport _getPatientReport = new GetPatientReport(_patientRepoMock.Object);

            PatientReport report = _getPatientReport.GetPatientsWithSymptoms();

            _patientRepoMock.Verify(m => m.GetPatientsWithSymptoms(), Times.Once);
        }

        [TestMethod]
        public void GetPatientsThatBrokeIsolation_calls_CorrectRepoMethod()
        {
            Mock<IPatientRepo> _patientRepoMock = new Mock<IPatientRepo>();
            _patientRepoMock.Setup(m => m.GetPatientsThatBrokeIsolation()).Returns(new PatientReport());
            GetPatientReport _getPatientReport = new GetPatientReport(_patientRepoMock.Object);

            PatientReport report = _getPatientReport.GetPatientsThatBrokeIsolation();

            _patientRepoMock.Verify(m => m.GetPatientsThatBrokeIsolation(), Times.Once);
        }
    }
}
