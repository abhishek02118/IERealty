using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using IERealtyDatabases.Databases;
using IERealtyDatabases.Models;
using IERealtyWCFService.EmailManager;

namespace IERealtyWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Booking" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Booking.svc or Booking.svc.cs at the Solution Explorer and start debugging.
    public class Booking : IBooking
    {
        public void BookAppointment(AppointmentModel bookAppointment)
        {
            using (var dbManager = DatabaseFactory.GetDatbase(DbType.SqlServer))
            {
                dbManager.BookAppointment(bookAppointment);
                
                //ThreadStart th=new ThreadStart(EmailAppointmentConfirmation.SendAppointmentConfirmationEmail(bookAppointment));
                Thread thread = new Thread(() => EmailAppointmentConfirmation.SendAppointmentConfirmationEmail(bookAppointment));
                thread.Start();
            }

        }

        public AppointmentModel GetNewAppointmentDetails()
        {
            using (var dbManager = DatabaseFactory.GetDatbase(DbType.SqlServer))
            {
                return dbManager.GetNewAppointmentDetails();
            }
        }
    }
}
