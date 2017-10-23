using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IERealtyDatabases.Models;
using IERealtyWCFService.EmailManager.Email;

namespace IERealtyWCFService.EmailManager
{
    public class EmailAppointmentConfirmation
    {

        //private const string EmailUniqueKeySubstring = "ContactRequest_ToEmail";
        private const string EmailUniqueKeySubstring = "_ContactRequest_ToEmail";
        private const string SubJectForCustomerInterestInProperty = "Willing to buy property_";
        private const string EmailFrom = "Administrator";
        //private Dictionary<string, string> _masterSettings;
        //private static Logger logger = LogManager.GetLogger("ContactUsService");

        //private static MailComponent _mailComponent = new MailComponent();
        private static MailSender _mailComponent = new MailSender();



        /*private static List<string> GetEmailList(Dictionary<string, string> _masterSettings)
        {

            string tempEmailUniqueKeySubstring = ConfigurationManagerUtility.GetConfiguredValue(
                DdfTagConstant.WebConfigKeyNameDbKeyCustomerFirstPartName)
                                                 + EmailUniqueKeySubstring.ToUpper();
            if (_masterSettings != null && _masterSettings.Count > 0)
            {
               
                return (from keys in _masterSettings.Keys
                        where keys.ToUpper().Contains(tempEmailUniqueKeySubstring.ToUpper())
                        select _masterSettings[keys]).ToList();
            }

            return null;
        }

        public static void SendMailToAdministrator(ContactUsViewModel contactUsViewModel)
        {
            try
            {
                Dictionary<string, string> _masterSettings = DbManager.GetMasterSettings();

                List<string> emailList = GetEmailList(_masterSettings);

                if (emailList != null && emailList.Count > 0)
                {
                    string toemail = emailList[0];
                    
                    List<string> cclist = null;
                    if (emailList.Count > 1)
                    {
                        cclist = emailList.GetRange(1, emailList.Count - 1);
                    }
                    SendEmail(toemail, cclist,
                        SubJectForCustomerInterestInProperty + contactUsViewModel.Name + "_" + contactUsViewModel.Phone
                        ,
                        "Congratulations Team," + "<br/><br/>  Customer " + contactUsViewModel.Name +
                        " has shown interest to purshase property." +
                        "<br/> Please see their contact informations and interests below. <br/><br/>" +

                        "Name       :   " + contactUsViewModel.Name + "<br/>" +
                        "Email      :   " + contactUsViewModel.Email + "<br/>" +
                        "Phone      :   " + contactUsViewModel.Phone + "<br/>" +
                        "Subject    :   " + contactUsViewModel.Subject + "<br/>" +
                        "Message    :   " + contactUsViewModel.Message + "<br/><br/><br/>" +
                        "Please contact as soon as possible.<br/><br/>" +
                        "Thanks,<br/>" +
                        EmailFrom, _masterSettings);
                }
            }
            catch (Exception ex)
            {
                //Do Nothing

            }
        }*/

        public static void SendAppointmentConfirmationEmail(object appointmentModelobj)
        {
            AppointmentModel appointmentModel = (AppointmentModel) appointmentModelobj;
            StringBuilder body = new StringBuilder("<body><h3>Hi,</h3><br/><p>Your appointment has been confirmed!</p><br/><p><strong>Date:   </strong>");
            
            body.Append(appointmentModel.AppointmentDate);
            body.Append("</p><p><strong>Time : </strong>");
            body.Append(appointmentModel.AppointmentTime);
            body.Append("</p><p><strong>Your Mobile :</strong>");
            body.Append(appointmentModel.Mobile);
            body.Append("</p><p><strong>You Message :</strong>");
            body.Append(appointmentModel.Message);
            body.Append("</p><br/><p><strong>Agent : </strong>");
            body.Append(appointmentModel.AgentName);
            body.Append("</p><p><strong>Agent Phone :</strong>");
            body.Append(appointmentModel.RealtorPhoneNumber);
            body.Append("</p><p><strong>Address :</strong>");
            body.Append(appointmentModel.RealtorAddress);
            body.Append("</p><br/><h3>Cheers!!<h3></body>");



            SendEmail(appointmentModel.Email, new List<string>() {"abhishek021186@gmail.com"},
                "Appointment Confirmation", body.ToString(), null);
        }

        private static bool SendEmail(string toEmailId, List<string> ccList, string subject, string body, Dictionary<string, string> _masterSettings)
        {
            try
            {

                MailAttribute attribute = new MailAttribute();
                attribute.SmtpServer = "smtp.gmail.com";
                attribute.SmtpServerPort = "587";

                attribute.Subject = subject;
                attribute.Body = body;
                //attribute.FromEmailId = DictionaryDataRetrival.GetValue(_masterSettings, "FromEmailAddress");
                //attribute.PassWord = DictionaryDataRetrival.GetValue(_masterSettings, "FromEmailPassword");

                attribute.FromEmailId = "abhishektechiess@gmail.com";
                attribute.PassWord = "chintukumarsinhaa";
                attribute.ToEmailId = toEmailId;
                if (ccList != null)
                {

                    attribute.CcList = ccList;
                    //attribute.CcList = null;
                }
                //attribute.MailFormat=MailFormat.Html;
                attribute.AttachmentPath = string.Empty;

                _mailComponent.SendEmail(attribute);

                return false;
            }
            catch (Exception ex)
            {
                //logger.Error("Inside SendNotification:: SendEmail-->" + ex.Message);
                throw ex;
            }
        }

        
    }
}