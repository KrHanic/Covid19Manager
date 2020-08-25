using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class RemovePatientFromIsolationVM
    {
        public string Patient_id { get; set; }
        public string OIB { get; set; }
        public string Name { get; set; }
        public string IsolationaAddress { get; set; }
    }
}
