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
    
    public partial class tblAgentAvaialbleForAppointment
    {
        public tblAgentAvaialbleForAppointment()
        {
            this.tblCustomerAppointments = new HashSet<tblCustomerAppointment>();
        }
    
        public long Id { get; set; }
        public long AgentId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblAgent tblAgent { get; set; }
        public virtual ICollection<tblCustomerAppointment> tblCustomerAppointments { get; set; }
    }
}
