using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IERealtyDatabases.Databases.SqlServer;

namespace IERealtyDatabases.Models
{

    [DataContract]
    public class AppointmentModel
    {
        /*[DataMember]
        public tblCustomerAppointment CustomerAppointment { get; set; }
        [DataMember]
        public string RealtorAddress { get; set; }
        [DataMember]
        public string AgentName { get; set; }
        [DataMember]
        public string RealtorName { get; set; }
        [DataMember]
        public string RealtorPhoneNumber { get; set; }*/

        [DataMember]
        public string AppointmentDate { get; set; }
        [DataMember]
        public string AppointmentTime { get; set; }
        [DataMember]
        public long DurationInMinutes { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Mobile { get; set; }




        [DataMember]
        public long AgentId { get; set; }
        [DataMember]
        public string AgentName { get; set; }

        [DataMember]
        public string RealtorAddress { get; set; }
        [DataMember]
        public string RealtorName { get; set; }
        [DataMember]
        public string RealtorPhoneNumber { get; set; }




    }
}
