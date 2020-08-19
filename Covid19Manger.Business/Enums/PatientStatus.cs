using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Common
{
    public enum PatientStatus
    {
        NotRegistered = 1,
        Registered = 2,
        RemovedFromIsolation = 3
    }
}
