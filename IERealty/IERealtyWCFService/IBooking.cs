using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IERealtyDatabases.Models;

namespace IERealtyWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBooking" in both code and config file together.
    [ServiceContract]
    public interface IBooking
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "BookAppointment",RequestFormat = WebMessageFormat.Json,ResponseFormat = WebMessageFormat.Json)]
        void BookAppointment(AppointmentModel appointmentModel);
        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetNewAppointmentDetails", RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        AppointmentModel GetNewAppointmentDetails();
    }
}
