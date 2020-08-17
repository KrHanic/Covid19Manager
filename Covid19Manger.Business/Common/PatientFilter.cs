using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Common
{
    public class PatientFilter
    {
        public int PatientId { get; set; }
        public long TimeFrom { get; set; }
        public long TimeTo { get; set; }
    }
}
