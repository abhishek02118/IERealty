//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IERealtyDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAddress
    {
        public tblAddress()
        {
            this.tblAgents = new HashSet<tblAgent>();
            this.tblBrokers = new HashSet<tblBroker>();
        }
    
        public long Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<tblAgent> tblAgents { get; set; }
        public virtual ICollection<tblBroker> tblBrokers { get; set; }
    }
}
