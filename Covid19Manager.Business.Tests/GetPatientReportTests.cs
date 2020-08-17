using Covid19Manager.Business.Common;
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
            PatientFilter filter = new PatientFilter();
            _patientRepoMock.Setup(m => m.GetPatientsWithSymptoms(filter)).Returns(new PatientReport());
            GetPatientReport _getPatientReport = new GetPatientReport(_patientRepoMock.Object);

            PatientReport report = _getPatientReport.GetPatientsWithSymptoms(filter);

            _patientRepoMock.Verify(m => m.GetPatientsWithSymptoms(filter), Times.Once);
        }

        [TestMethod]
        public void GetPatientsThatBrokeIsolation_calls_CorrectRepoMethod()
        {
            Mock<IPatientRepo> _patientRepoMock = new Mock<IPatientRepo>();
            PatientFilter filter = new PatientFilter();
            _patientRepoMock.Setup(m => m.GetPatientsThatBrokeIsolation(filter)).Returns(new PatientReport());
            GetPatientReport _getPatientReport = new GetPatientReport(_patientRepoMock.Object);

            PatientReport report = _getPatientReport.GetPatientsThatBrokeIsolation(filter);

            _patientRepoMock.Verify(m => m.GetPatientsThatBrokeIsolation(filter), Times.Once);
        }
    }
}
