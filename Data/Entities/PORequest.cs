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
    
    public partial class PoRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PoRequest()
        {
            this.Quotes = new HashSet<Quote>();
        }
    
        public int PoRequestID { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> FinishedDate { get; set; }
        public int RequestStatusID { get; set; }
        public Nullable<int> SelectedQuoteID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<int> StaffSubmitID { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Order Order { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
