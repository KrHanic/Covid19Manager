using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Covid19Manager.Business.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public long OIB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsolationAddress { get; set; }
        public double? IsolationLat { get; set; }
        public double? IsolationLong { get; set; }
        public Location LastLocation { get; set; }
        public int StatusId { get; set; }
        public string StatusDescription { get; set; }
        public Condition LastCondition { get; set; }
        public List<Condition> ConditionHistory { get; set; }
    }
}
