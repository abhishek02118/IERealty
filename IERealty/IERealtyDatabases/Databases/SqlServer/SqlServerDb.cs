using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IERealtyDatabases.Models;

namespace IERealtyDatabases.Databases.SqlServer
{
    class SqlServerDb:IDbManager
    {
        private bool disposed = false;

        public bool BookAppointment(AppointmentModel appointmentModel)
        {

            DateTime dateTime = DateTime.ParseExact(appointmentModel.AppointmentTime,
                                    "hh:mm tt", CultureInfo.InvariantCulture);
            TimeSpan appointmentTime = dateTime.TimeOfDay;
            using (var dbContext=new IERealtyDbContext())
            {

                tblCustomerAppointment customerAppointment=new tblCustomerAppointment()
                {
                    AppointmentDate = Convert.ToDateTime(appointmentModel.AppointmentDate),
                    AppointmentTime = appointmentTime,
                    AvailableAgentId = appointmentModel.AgentId,
                    DurationInMinutes = appointmentModel.DurationInMinutes,
                    Email = appointmentModel.Email,
                    Mobile = appointmentModel.Mobile,
                    Message = appointmentModel.Message,
                    IsActive = true
                };
                dbContext.tblCustomerAppointments.Add(customerAppointment);
                dbContext.SaveChanges();
                //dbContext.tblCustomerAppointments.AddOrUpdate(appointmentModel.CustomerAppointment);
                //dbContext.SaveChanges();
            }
            return true;
        }

        public AppointmentModel GetNewAppointmentDetails()
        {
            AppointmentModel customerAppointment=new AppointmentModel()
            {
                //CustomerAppointment=new tblCustomerAppointment()
            };
            
            tblAgentAvaialbleForAppointment getOnlyOneAvailableAgentFromTop = null;
            tblAgent tbleAgentDetail = null;
            tblBroker tblBrokerDetail = null;
            tblAddress tblAddressDetail=null;

            using (var dbContext = new IERealtyDbContext())
            {

                getOnlyOneAvailableAgentFromTop=dbContext.tblAgentAvaialbleForAppointments.FirstOrDefault(
                    agentAvaialbleForAppointments => agentAvaialbleForAppointments.IsActive);

                if (getOnlyOneAvailableAgentFromTop != null)
                {
                    tbleAgentDetail =
                        dbContext.tblAgents.FirstOrDefault(agent => agent.Id == getOnlyOneAvailableAgentFromTop.AgentId);
                }
                if (tbleAgentDetail != null)
                {
                    tblBrokerAgent tblBrokerAgentDetail = (dbContext.tblBrokerAgents.SingleOrDefault(ba => ba.AgentId == tbleAgentDetail.Id));
                    tblBrokerDetail =
                        dbContext.tblBrokers.SingleOrDefault(
                            broker =>
                                broker.Id == tblBrokerAgentDetail.BrokerId
                                );
                }
                if (tblBrokerDetail != null)
                {
                    tblAddressDetail =
                        dbContext.tblAddresses.SingleOrDefault(adress => adress.Id == tblBrokerDetail.AddressId);
                }

            }

            if (tbleAgentDetail == null || tblBrokerDetail==null || tblAddressDetail==null) return null;
            //customerAppointment.CustomerAppointment.AvailableAgentId = getOnlyOneAvailableAgentFromTop.AgentId;
            //customerAppointment.CustomerAppointment.IsActive = true;
            customerAppointment.AgentId = getOnlyOneAvailableAgentFromTop.Id;
            customerAppointment.AgentName = tbleAgentDetail.Name;
            customerAppointment.RealtorName = tblBrokerDetail.Name;
            customerAppointment.RealtorAddress = tblAddressDetail.Address;
            customerAppointment.RealtorPhoneNumber = tblBrokerDetail.Phone;

            return customerAppointment;


           
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                
            }

            disposed = true;
        }
        public void Dispose()
        {
           Dispose(true);
           GC.SuppressFinalize(this);
        }

        ~SqlServerDb()
        {
            Dispose(false);
        }
    }
}
