//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flat()
        {
            this.Contracts = new HashSet<Contract>();
        }
    
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public Nullable<int> Bedroom { get; set; }
        public Nullable<bool> Parking { get; set; }
        public Nullable<bool> PetAllowed { get; set; }
        public Nullable<bool> BillsIncluded { get; set; }
        public Nullable<bool> Furnished { get; set; }
    
        public virtual Building Building { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
