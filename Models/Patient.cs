using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team6JQDental.Models
{
    public class Patient
    {
        public int Patient_ID { get; set; }
        public Nullable<int> Account_ID { get; set; }
        public string Patient_First_Name { get; set; }
        public string Patient_Middle_Name { get; set; }
        public string Patient_Last_Name { get; set; }
        public string Patient_Street { get; set; }
        public string Patient_City { get; set; }
        public string Patient_State { get; set; }
        public string Patient_Zip { get; set; }
        public string Patient_Phone_Primary { get; set; }
        public string Patient_Phone_Secondary { get; set; }
        public string Patient_SSN { get; set; }
        public Nullable<System.DateTime> Patient_DOB { get; set; }
        public Nullable<bool> Patient_Minor { get; set; }
        public string Patient_Head_Of_House { get; set; }
        public string Patient_Allergies { get; set; }

    }
}