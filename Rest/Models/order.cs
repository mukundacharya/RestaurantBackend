//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        public int orderID { get; set; }
        public Nullable<int> customerID { get; set; }
        public Nullable<int> foodID { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<decimal> amount { get; set; }
    
        public virtual customerInfo customerInfo { get; set; }
        public virtual menu menu { get; set; }
    }
}
