using System;

namespace Team6JQDental.Models
{
    public class AccountInfo
    {
        public String FirstName { get; set; }
        public Nullable<decimal> Account_Balance { get; set; }
        public Nullable<decimal> Last_Payment { get; set; }
        public Nullable<decimal> Remaining_Balance { get; set; }
    }
}