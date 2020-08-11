using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Covid19Manager.Business.Entities;
using System.Linq;
using Moq;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.UseCases;

namespace Covid19Manager.Business.Tests
{
    [TestClass]
    public class GetPatientsTests
    {
        Mock<IPatientRepo> _patientRepoMock;

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void ByStatus_callsCorrectRepoMethod(int status)
        {
            _patientRepoMock = new Mock<IPatientRepo>();
            _patientRepoMock.Setup(m => m.GetPatientsByStatus(status)).Returns(new List<Patient>());
            GetPatients getPatients = new GetPatients(_patientRepoMock.Object);

            getPatients.ByStatus(status);

            _patientRepoMock.Verify(m => m.GetPatientsByStatus(status), Times.Once);
        }
    }
}
