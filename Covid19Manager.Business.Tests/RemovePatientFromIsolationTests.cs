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
    public class RemovePatientFromIsolationTests
    {
        [TestMethod]
        [DataRow(1000)]
        [DataRow(1001)]
        [DataRow(1002)]
        public void Execute_calls_CorrectRepoMethod(int id)
        {
            Mock<IPatientRepo> _patientRepo = new Mock<IPatientRepo>();
            RemovePatientFromIsolation removePatientFromIsolation = 
                new RemovePatientFromIsolation(_patientRepo.Object);

            removePatientFromIsolation.Execute(id);

            _patientRepo.Verify(m => m.RemovePatientFromIsolation(id), Times.Once);
        }
    }
}
