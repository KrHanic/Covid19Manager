using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Common
{
    public enum PatientStatus
    {
        Registered = 1,
        NotRegistered = 2,
        RemovedFromIsolation = 3
    }
}
