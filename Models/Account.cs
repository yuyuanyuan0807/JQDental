using System;

namespace Team6JQDental.Models
{
    public class Account
    {
        public int Account_ID { get; set; }
        public Nullable<decimal> Account_Balance { get; set; }
        public string Password { get; set; }
        public Patient patient { get; set; }
    }
}