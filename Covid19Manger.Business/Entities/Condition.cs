using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Manager.Business.Entities
{
    public class Condition
    {
        public double Temperature { get; set; }
        public bool Cough { get; set; }
        public bool Fatigue { get; set; }
        public bool MusclePain { get; set; }
        public long Time { get; set; }
    }
}
