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
    
    public partial class AssetRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssetRequest()
        {
            this.Quotes = new HashSet<Quote>();
        }
    
        public int AssetRequestID { get; set; }
        public string RequestDate { get; set; }
        public string Purpose { get; set; }
        public int PersonnelID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public int RequestStatusID { get; set; }
    
        public virtual Personnel Personnel { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual Product Product { get; set; }
    }
}
