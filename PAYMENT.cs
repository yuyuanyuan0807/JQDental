//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team6JQDental
{
    using System;
    using System.Collections.Generic;
    
    public partial class PAYMENT
    {
        public int Payment_ID { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public Nullable<decimal> Payment_Amount { get; set; }
        public Nullable<int> Account_ID { get; set; }
    
        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}