using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using IERealty.IERealtyBookingService;

namespace IERealty.Models
{

    public class CommonBookingDetail
    {


        public static List<string> GetAppointmentType()
        {
            return new List<string>()
            {
                "Open listing","Exclusive agency","Sole agency"
            };
        }

        public static Hashtable GetDuration()
        {

            return new Hashtable(){{"15 minutes", 15},{"30 minutes",30},{"45 minutes",45},{"1 hour",60}};
        }

        public static List<string> GetTimeList()
        {
            return new List<string>()
            {
                "05:00 AM","05:15 AM","05:30 AM","05:45 AM","06:00 AM","06:15 AM","06:30 AM","06:45 AM",
                "07:00 AM","07:15 AM","07:30 AM","07:45 AM","08:00 AM","08:15 AM","08:30 AM","08:45 AM","09:00 AM","09:15 AM","09:30 AM","09:45 AM","10:00 AM",
                "10:15 AM","10:30 AM","10:45 AM","11:00 AM","11:15 AM","11:30 AM","11:45 AM","12:00 PM","12:15 PM","12:30 PM","12:45 PM",
                "01:00 PM","01:15 PM","01:30 PM","01:45 PM","02:00 PM","02:15 PM","02:30 PM","02:45 PM","03:00 PM","03:15 PM","03:30 PM","03:45 PM",
                "04:00 PM","04:15 PM","04:30 PM","04:45 PM","05:00 PM","05:15 PM","05:30 PM","05:45 PM","06:00 PM","06:15 PM","06:30 PM","06:45 PM","07:00 PM",
                "07:15 PM","07:30 PM","07:45 PM","08:00 PM","08:15 PM","08:30 PM","08:45 PM","09:00 PM","09:15 PM","09:30 PM","09:45 PM",
                "10:00 PM","10:15 PM","10:30 PM","10:45 PM","11:00 PM","11:15 PM","11:30 PM","11:45 PM"
            };
        }
    }

    public class TimeListDropDown
    {
        [Display(Name = "SelectedTime")]
        [Required(ErrorMessage = "Please Select time")]
        public string SelectedTime { get; set; }

        public List<string> TimeList { get; set; } 
    }
    public class DurationListDropDown
    {

        [Display(Name = "SelectedDuration")]
        [Required(ErrorMessage = "Please Select duration")]
        public string SelectedDuration { get; set; }
        public Hashtable DurationList { get; set; }
    }
    public class AppointmentTypeDropDown
    {

        [Display(Name = "SelectedAppointmentType")]
        [Required(ErrorMessage = "Please Select appointment type")]
        public string SelectedAppointmentType { get; set; }
        public List<string> AppointmentTypeList { get; set; }
    }

    public class AppointmentModel
    {
        public AppointmentModel()
        {
            TimeListDropDown=new TimeListDropDown();
        }
        [Display(Name="AppointmentDate")]
        [Required(ErrorMessage = "Please Select Date")]
        public string AppointmentDate { get; set; }

        [Display(Name = "AppointmentTime")]
        [Required(ErrorMessage = "Please Select Time")]
        public string AppointmentTime { get; set; }

        [Display(Name = "DurationInMinutes")]
        public long DurationInMinutes{ get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address,please try again")]
        [Required(ErrorMessage = "Please Enter Email address")]
        public string Email{ get; set; }

        [Display(Name = "Message")]
        public string Message{ get; set; }

        [Display(Name = "Mobile")]
        [Phone(ErrorMessage = "Invalid phone number, please enter correctly")]
        [Required(ErrorMessage = "Please Enter phone number")]
        public string Mobile{ get; set; }





        [Display(Name = "AgentId")]
        public long AgentId { get; set; }
        [Display(Name = "AgentName")]
        public string AgentName { get; set; }


        [Display(Name = "RealtorAddress")]
        public string RealtorAddress { get; set; }
        [Display(Name = "RealtorName")]
        public string RealtorName { get; set; }
        [Display(Name = "RealtorPhoneNumber")]
        public string RealtorPhoneNumber { get; set; }


        public TimeListDropDown TimeListDropDown { get; set; }
        public DurationListDropDown DurationListDropDown { get; set; }
        public AppointmentTypeDropDown AppointmentTypeDropDown1 { get; set; }
        public AppointmentTypeDropDown AppointmentTypeDropDown2 { get; set; }
        public AppointmentTypeDropDown AppointmentTypeDropDown3 { get; set; }

        //{"GetNewAppointmentDetailsResult":{"AgentName":"Frank","CustomerAppointment":{"AppointmentDate":"\/Date(-62135578800000-0500)\/","AppointmentTime":"PT0S","AvailableAgentId":101,"DurationInMinutes":0,"Email":null,"Id":0,"IsActive":true,"Message":null,"Mobile":null,"tblAgentAvaialbleForAppointment":null},"RealtorAddress":"20 Stone Hill Court,Scarborough","RealtorName":"IERealty Inc","RealtorPhoneNumber":"6476426056"}}
    }
}