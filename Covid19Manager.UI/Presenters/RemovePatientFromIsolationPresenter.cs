using Covid19Manager.Business.Entities;
using Covid19Manager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.Presenters
{
    public class RemovePatientFromIsolationPresenter
    {
        public RemovePatientFromIsolationVM Present(Patient patient)
        {
            return new RemovePatientFromIsolationVM()
            {
                Patient_id = patient.Id.ToString(),
                OIB = patient.OIB.ToString(),
                Name = $"{patient.LastName} {patient.FirstName}",
                IsolationaAddress = patient.IsolationAddress
            };
        }
    }
}
