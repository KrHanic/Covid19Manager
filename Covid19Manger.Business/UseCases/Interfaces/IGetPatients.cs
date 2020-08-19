using Covid19Manager.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases.Interfaces
{
    public interface IGetPatients
    {
        List<Patient> ByStatus(int status);
    }
}
