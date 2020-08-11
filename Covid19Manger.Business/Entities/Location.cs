using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Entities
{
    public class Location
    {
        public double PatientCurrentLat { get; set; }
        public double PatientCurrentLong { get; set; }
        public long Time { get; set; }
    }
}
