using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Covid19Manager.Business.UseCases;
using Covid19Manager.Business.Repositories;
using Moq;
using Covid19Manager.Business.Entities;

namespace Covid19Manager.Business.Tests
{
    [TestClass]
    public class GetPatientDetailsTests
    {
        [TestMethod]
        [DataRow(1000)]
        [DataRow(1001)]
        [DataRow(1002)]
        [DataRow(1003)]
        public void Execute_calls_correctRepoMethod(int id)
        {
            Mock<IPatientRepo> _patientRepoMock = new Mock<IPatientRepo>();
            _patientRepoMock.Setup(m => m.GetPatientDetails(id)).Returns(new Patient());
            GetPatientDetails _getPatientDetails = new GetPatientDetails(_patientRepoMock.Object);

            _getPatientDetails.Execute(id);

            _patientRepoMock.Verify(m => m.GetPatientDetails(id), Times.Once);
        }
    }
}
