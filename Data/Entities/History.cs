//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class History
    {
        public int HistoryID { get; set; }
        public Nullable<System.DateTime> Checkout_Date { get; set; }
        public Nullable<System.DateTime> Checkin_Date { get; set; }
        public string Comment { get; set; }
        public int AssetID { get; set; }
        public int EmployeeID { get; set; }
    
        public virtual Asset Asset { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
