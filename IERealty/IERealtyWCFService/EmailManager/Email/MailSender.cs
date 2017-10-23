//using System.Web.Mail;
using System;
using System.Net;
using System.Net.Mail;
using MailMessage = System.Net.Mail.MailMessage;

namespace IERealtyWCFService.EmailManager.Email
{
    public class MailSender
    {
        public bool SendEmail(MailAttribute objMailAttribute)
        {
            try
            {
               
                MailAddress from = new MailAddress(objMailAttribute.FromEmailId);
                MailAddress to = new MailAddress(objMailAttribute.ToEmailId);
                MailMessage message = new MailMessage(from, to);
                
                message.Subject = objMailAttribute.Subject;
                message.Body = objMailAttribute.Body;
                message.IsBodyHtml = true;
                string cc = "";
                if (objMailAttribute.CcList != null)
                {
                    for (int counter = 0; counter < objMailAttribute.CcList.Count; counter++)
                    {
                        if (counter == 0)
                        {
                            cc = objMailAttribute.CcList[counter];
                        }
                        else
                        {
                            cc += "," + objMailAttribute.CcList[counter];
                        }
                    }
                    message.CC.Add(cc);
                }
                //message.CC.Add(cc);

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                // Include credentials if the server requires them.
                //client.Credentials = CredentialCache.DefaultNetworkCredentials;
                var nc = new NetworkCredential(objMailAttribute.FromEmailId, objMailAttribute.PassWord);

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = nc;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

