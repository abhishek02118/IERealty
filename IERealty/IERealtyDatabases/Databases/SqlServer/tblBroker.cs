//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IERealtyDatabases.Databases.SqlServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBroker
    {
        public tblBroker()
        {
            this.tblBrokerAgents = new HashSet<tblBrokerAgent>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public long AddressId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblAddress tblAddress { get; set; }
        public virtual ICollection<tblBrokerAgent> tblBrokerAgents { get; set; }
    }
}
