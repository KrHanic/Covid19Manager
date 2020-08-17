using Covid19Manager.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.UseCases.Interfaces
{
    public interface ICreatePatient
    {
        int Execute(Patient patient);
    }
}
