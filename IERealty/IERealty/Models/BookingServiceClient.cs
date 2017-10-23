using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.ServiceModel;
using System.Web.Script.Serialization;

namespace IERealty.Models
{
    public class BookingServiceClient
    {

        private static string Base_URL = "http://localhost:58370/Booking.svc/";
        public static AppointmentModel GetNewBookingModel()
        {
            try
            {
                var webClient = new WebClient();
                var json = webClient.DownloadString(Base_URL + "GetNewAppointmentDetails");
                var js = new JavaScriptSerializer();
                AppointmentModel appointmentModel= (AppointmentModel)js.Deserialize(json,typeof(AppointmentModel));
                
                
                appointmentModel.TimeListDropDown = new TimeListDropDown()
                {
                    TimeList = CommonBookingDetail.GetTimeList()
                };
                appointmentModel.AppointmentTypeDropDown1 = new AppointmentTypeDropDown()
                {
                    AppointmentTypeList = CommonBookingDetail.GetAppointmentType()
                };
                appointmentModel.AppointmentTypeDropDown2 = new AppointmentTypeDropDown()
                {
                    AppointmentTypeList = CommonBookingDetail.GetAppointmentType()
                };
                appointmentModel.AppointmentTypeDropDown3 = new AppointmentTypeDropDown()
                {
                    AppointmentTypeList = CommonBookingDetail.GetAppointmentType()
                };
                appointmentModel.DurationListDropDown = new DurationListDropDown()
                {
                    DurationList = CommonBookingDetail.GetDuration()
                };
                return appointmentModel;

            }
            catch
            {
                return null;
            }
        }

        public static bool RegisterNewAppointment(AppointmentModel appointmentModel)
        {

            try
            {
                appointmentModel.DurationInMinutes = int.Parse(appointmentModel.DurationListDropDown.SelectedDuration);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AppointmentModel));
                
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, appointmentModel);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(Base_URL + "BookAppointment", "POST", data);
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}