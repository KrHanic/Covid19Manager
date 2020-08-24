using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class PatientDetailsConditionVM
    {
        [DisplayName("Temperatura")]
        public string Temperature { get; set; }
        [DisplayName("Vrijeme očitanja")]
        public string Time { get; set; }
    }
}
