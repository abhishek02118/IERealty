using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel;
using IERealty.Models;
using System.Web.Script.Serialization;

namespace IERealty.Controllers
{
    public class BookingController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            var appointmentModel=BookingServiceClient.GetNewBookingModel();
            return View(appointmentModel);
        }

        [HttpPost]
        public ActionResult Index(AppointmentModel appointmentModel)
        {

            if (ModelState.IsValid)
            {
                if (BookingServiceClient.RegisterNewAppointment(appointmentModel))
                {
                    return RedirectToAction("Index");
                }
               
            }
            appointmentModel.AppointmentTypeDropDown1.AppointmentTypeList = CommonBookingDetail.GetAppointmentType();
            appointmentModel.AppointmentTypeDropDown2.AppointmentTypeList = CommonBookingDetail.GetAppointmentType();
            appointmentModel.AppointmentTypeDropDown3.AppointmentTypeList = CommonBookingDetail.GetAppointmentType();
            appointmentModel.DurationListDropDown.DurationList = CommonBookingDetail.GetDuration();
            appointmentModel.TimeListDropDown.TimeList = CommonBookingDetail.GetTimeList();
            return View(appointmentModel);
        }

    }
}
