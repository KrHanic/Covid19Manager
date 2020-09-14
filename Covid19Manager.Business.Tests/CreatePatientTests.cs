using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Tests
{
    [TestClass]
    public class CreatePatientTests
    {
        Patient patient;
        Mock<IPatientRepo> _patientRepo;
        CreatePatient createPatient;

        [TestInitialize]
        public void SetUp()
        {
            patient = new Patient() { 
                FirstName = "Pero",
                LastName = "Peric",
                IsolationAddress = "adresa",
                OIB = 11111111111,
                IsolationLat = 12.12,
                IsolationLong = 12.12
            };
            _patientRepo = new Mock<IPatientRepo>();
            _patientRepo.Setup(m => m.CreatePatient(patient)).Returns(1);
            createPatient = new CreatePatient(_patientRepo.Object);
        }

        [TestMethod]
        public void Execute_calls_CorrectRepoMethod()
        {
            createPatient.Execute(patient);

            _patientRepo.Verify(m => m.CreatePatient(patient), Times.Once);
        }

        [TestMethod]
        public void Execute_sets_PatientStatusIdTo1()
        {
            createPatient.Execute(patient);

            AreEqual(2, patient.StatusId);
        }

        [TestMethod]
        public void Execute_returns_PatientIdIfCreated()
        {
            int patientId = createPatient.Execute(patient);

            AreEqual(1, patientId);
        }

        [TestMethod]
        [MyExpectedException(typeof(ArgumentException), "Polje ime nije popunjejno.")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Execute_Throws_IfFirstNameIsNullOrEmpty(string firstName)
        {
            patient.FirstName = firstName;

            createPatient.Execute(patient);
        }

        [TestMethod]
        [MyExpectedException(typeof(ArgumentException), "Polje prezime nije popunjejno.")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Execute_Throws_IfLastNameIsNullOrEmpty(string lastName)
        {
            patient.LastName = lastName;

            createPatient.Execute(patient);
        }

        [TestMethod]
        [MyExpectedException(typeof(ArgumentException), "Polje adresa samoizolacije nije popunjejno.")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Execute_Throws_IfIsolationAddressIsNullOrEmpty(string address)
        {
            patient.IsolationAddress = address;

            createPatient.Execute(patient);
        }

        [TestMethod]
        [MyExpectedException(typeof(ArgumentException), "Polje oib nije popunjejno ili oib nije ispravan.")]
        [DataRow(null)]
        [DataRow(0)]
        [DataRow(123)]
        public void Execute_Throws_IfOIBIsNullOrNot11CharLong(long oib)
        {
            patient.OIB = oib;

            createPatient.Execute(patient);
        }

        [TestMethod]
        [MyExpectedException(typeof(ArgumentException), "Polje latituda nije popunjejno ili latituda nije ispravana.")]
        [DataRow(null)]
        [DataRow(-90.01)]
        [DataRow(90.01)]
        public void Execute_Throws_IfIsolationLatIsNullOrNotInRange(double? lat)
        {
            patient.IsolationLat = lat;

            createPatient.Execute(patient);
        }

        [TestMethod]
        [MyExpectedException(typeof(ArgumentException), "Polje longituda nije popunjejno ili longituda nije ispravana.")]
        [DataRow(null)]
        [DataRow(-180.01)]
        [DataRow(180.01)]
        public void Execute_Throws_IfIsolationLongIsNullOrNotInRange(double? IsoLong)
        {
            patient.IsolationLong = IsoLong;

            createPatient.Execute(patient);
        }
    }

    public sealed class MyExpectedException : ExpectedExceptionBaseAttribute
    {
        private Type _expectedExceptionType;
        private string _expectedExceptionMessage;

        public MyExpectedException(Type expectedExceptionType)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = string.Empty;
        }

        public MyExpectedException(Type expectedExceptionType, string expectedExceptionMessage)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = expectedExceptionMessage;
        }

        protected override void Verify(Exception exception)
        {
            IsNotNull(exception);

            IsInstanceOfType(exception, _expectedExceptionType, "Wrong type of exception was thrown.");

            if (!String.IsNullOrEmpty(_expectedExceptionMessage?.Trim()))
            {
                AreEqual(_expectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
            }
        }
    }
}
