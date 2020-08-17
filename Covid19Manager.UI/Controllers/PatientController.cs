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
            return View();
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
                patientVM.ID = createPatient.Execute(new Business.Entities.Patient() { 
                                    OIB = patientVM.OIB,
                                    FirstName = patientVM.FirstName,
                                    LastName = patientVM.LastName,
                                    IsolationAddress = patientVM.IsolationAddress,
                                    IsolationLat = patientVM.IsolationLat,
                                    IsolationLong = patientVM.IsolationLong
                });
                return View(createPatient);
            }
            else
                return View(patientVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
