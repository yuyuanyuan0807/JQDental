using System;


namespace Team6JQDental.Models
{
    /// <summary>
    /// Dentist Model Class to be used in API
    /// </summary>
    public class Dentist
    {
        public int DentistID { get; set;}

        public String FirstName
        {
            get;
            set;
        }

        public String LastName
        {
            get;
            set;
        }

        public String MiddleName
        {
            get;
            set;
        }

        public String JoinDate
        {
            get;
            set;
        }
    }
}