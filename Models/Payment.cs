using System;


namespace Team6JQDental.Models
{
    /// <summary>
    /// Dentist Model Class to be used in API
    /// </summary>
    public class Payment
    {
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public Nullable<decimal> Payment_Amount { get; set; }
        public Nullable<int> Account_ID { get; set; }
    }
}

