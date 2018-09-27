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
    
    public partial class Asset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asset()
        {
            this.AssetServices = new HashSet<AssetService>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Histories = new HashSet<History>();
        }
    
        public int AssetID { get; set; }
        public string CurrentPrice { get; set; }
        public Nullable<int> AuditID { get; set; }
        public string Barcode { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> AssetServiceID { get; set; }
        public int AssetStatusID { get; set; }
    
        public virtual AssetStatus AssetStatus { get; set; }
        public virtual Audit Audit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetService> AssetServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> Histories { get; set; }
        public virtual Product Product { get; set; }
    }
}
