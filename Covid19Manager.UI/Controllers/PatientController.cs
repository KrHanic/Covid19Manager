using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Covid19Manager.UI.Models;
using Covid19Manager.UI.ViewModels;
using Covid19Manager.Business.UseCases;
using Covid19Manager.Business.Repositories;
using Covid19Manager.Business.Entities;
using Covid19Manager.Business.Common;
using Covid19Manager.UI.Presenters;
using Covid19Manager.UI.Enums;

namespace Covid19Manager.UI.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepo _patientRepo;

        public PatientController(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            GetPatients getPatients = new GetPatients(_patientRepo);
            PatientTablePresenter patientTablePresenter = new PatientTablePresenter();

            List<Patient> patients = getPatients.ByStatus((int)PatientStatus.Registered);
            List<PatientTableVM> patientsVM = patientTablePresenter.SetRowColors(patients);

            return View(patientsVM);
        }

        [HttpGet]
        public IActionResult CreatePatient()
        {
            CreatePatientVM patientVM = new CreatePatientVM();
            return View(patientVM);
        }

        [HttpPost]
        public IActionResult CreatePatient(CreatePatientVM patientVM)
        {
            CreatePatient createPatient = new CreatePatient(_patientRepo);
            if (ModelState.IsValid) {
                patientVM.ID = createPatient.Execute(new Patient() { 
                                    OIB = patientVM.OIB.Value,
                                    FirstName = patientVM.FirstName,
                                    LastName = patientVM.LastName,
                                    IsolationAddress = patientVM.IsolationAddress,
                                    IsolationLat = patientVM.IsolationLat,
                                    IsolationLong = patientVM.IsolationLong
                });
                return View(patientVM);
            }
            else
                return View(patientVM);
        }

        [HttpGet]
        public IActionResult PatientDetails(int id)
        {
            GetPatientDetails getPatientDetails = new GetPatientDetails(_patientRepo);
            PatientDetailsPresenter presenter = new PatientDetailsPresenter();

            Patient patient = getPatientDetails.Execute(id);
            PatientDetailsVM details = presenter.Present(patient);

            return View(details);
        }

        [HttpGet]
        public IActionResult RemovePatientFromIsolation(int id)
        {
            GetPatientDetails getPatientDetails = new GetPatientDetails(_patientRepo);
            RemovePatientFromIsolationPresenter presenter = new RemovePatientFromIsolationPresenter();

            Patient patient = getPatientDetails.Execute(id);
            RemovePatientFromIsolationVM patientInfo = presenter.Present(patient);

            return View(patientInfo);
        }

        [HttpPost]
        public IActionResult RemovePatientFromIsolation(string patient_id)
        {
            RemovePatientFromIsolation removePatientFromIsolation = new RemovePatientFromIsolation(_patientRepo);

            removePatientFromIsolation.Execute(Int32.Parse(patient_id));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetPatientsMap()
        {
            GetPatients getPatients = new GetPatients(_patientRepo);
            PatientMapPresenter presenter = new PatientMapPresenter();

            List<Patient> patients = getPatients.ByStatus((int)PatientStatus.Registered);
            List<PatientMapVM> patientMapVMs = presenter.Present(patients);

            return View(patientMapVMs);
        }

        [HttpGet]
        public IActionResult GetReportDashboard(PatientDashboardVM dashboardVM)
        {
            PatientDashboardVM _dashboardVM;

            if (dashboardVM?.Filter?.TimeFrom != null)
                _dashboardVM = dashboardVM;
            else
                _dashboardVM = new PatientDashboardVM() {
                    Filter = new PatientFilter()
                };

            GetPatientReport getPatientReport = new GetPatientReport(_patientRepo);
            PatientDashboardPresenter presenter = new PatientDashboardPresenter(_dashboardVM);
            PatientReport report = new PatientReport();

            if (_dashboardVM.ReportTableId == (int)PatientReportTables.PatientsOutOfOneKmRadius)
                report = getPatientReport.GetPatientsThatBrokeIsolation(_dashboardVM.Filter);
            else
                report = getPatientReport.GetPatientsWithSymptoms(_dashboardVM.Filter);

            _dashboardVM = presenter.Present(report);

            return View(_dashboardVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
