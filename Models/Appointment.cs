using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team6JQDental.Models
{
    public class Appointment
    {
        public int Appointment_ID { get; set; }
        public Nullable<int> Dentist_ID { get; set; }
        public Nullable<int> Patient_ID { get; set; }
        public Nullable<System.DateTime> Appointment_Date { get; set; }
        public Nullable<System.DateTime> Appointment_Time { get; set; }
        public Dentist Dentist { get; set; }
        public Patient Patient { get; set; }
        public List<String> ScheduledServiceList { get; set; }
        public List<int> ScheduledServiceIDs { get; set; }
    }
}