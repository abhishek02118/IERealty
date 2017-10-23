using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IERealtyDatabases.Databases.SqlServer;
using IERealtyDatabases.Models;

namespace IERealtyDatabases
{
    public interface IDbManager:IDisposable
    {
        bool BookAppointment(AppointmentModel appointmentModel);
        AppointmentModel GetNewAppointmentDetails();
    }
}
