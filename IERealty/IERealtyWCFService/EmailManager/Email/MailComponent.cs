using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using MailMessage = System.Net.Mail.MailMessage;

namespace IERealtyWCFService.EmailManager.Email
{
    public class MailComponent
    {

        //DbManager _dbManager = new DbManager();
        //private int _notificationId = -1;
        //public int NotificationId { get { return _notificationId; } set { _notificationId = value; } }
       // public bool IsWindowsApplication { get; set; }



        public              string                          Subject             { get; set; }// Subject in Mail 
        public              string                          Body                { get; set; }   //Body in Mail
        public              bool                            IsHtml              { get; set; }  //Body content type
        public              string                          ToAddress           { get; set; }  //Multiple to address string by ";" seperated
        public              string                          BccAddress          { get; set; } //Multiple bcc address string by ";" seperated
        public              string                          CcAddress           { get; set; } //Multiple cc address string by ";" seperated
        public              string                          FromAddress         { get; set; }//From address in mail
        public              bool                            IsSmtpClientMail    { get; set; }//setting Mail Type(Net.mail or Web.mail)
        public              string                          UserName            { get; set; }//authentication credential for System.net.mail
        public              string                          Password            { get; set; }//authentication credential for System.net.mail
        public              int                             PortNumber          { get; set; }
        public              string                          ServerName          { get; set; }
        public              bool                            EnableSsl           { get; set; }
        private              List<HttpPostedFile>           _attachments;                       //Attachments List
        private              int                            ToFlag              { get; set; }
        private              int                            BccFlag             { get; set; }
        private              int                            CcFlag              { get; set; }
        private              List<string>                   _fileArray =        new List<string>();
        private              List<string>                   _attachmentFileName;



        public List<HttpPostedFile> Attachments
        {
            get { return _attachments ?? (_attachments = new List<HttpPostedFile>()); }//check _attachments is null then initialize attachments list
            set { _attachments = value; }
        }

        public List<string> AttachmentFileNames
        {
            get { return _attachmentFileName ?? (_attachmentFileName = new List<string>()); }//attachments file name is null then initialize the list
            set { _attachmentFileName = value; }
        }


        //Constructor
        public MailComponent()
        {
            ToFlag  = 0;
            BccFlag = 0;
            CcFlag  = 0;
        }

        /// <summary>
        /// An overloaded function send mail without any argument
        /// </summary>
        /// <returns>bool value</returns>
        public bool SendMail()
        {
            try
            {   //Check mail is system.net.mail type or system.web.mail type
                if (IsSmtpClientMail)   //if system.net.mail
                {
                    MailMessage mailMsg = new MailMessage();
                    //call the over loaded function send mail with one argument of system.net.mail.mailmessage type

                    // mailMsg.AlternateViews.Add(mailMsg);
                    bool response = SendMail(mailMsg);
                    if (response)
                    {
                        UpdateDatabase();
                    }
                    return response;
                }
                else
                {   //system.web.mail call another overloaded function
                    System.Web.Mail.MailMessage mailMessage = new System.Web.Mail.MailMessage();
                    //call the over loaded function send mail with one argument of system.web.mail.mailmessage type
                    bool response = SendMail(mailMessage);

                    if (response)
                    {
                        UpdateDatabase();
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateDatabase()
        {
            try
            {
                //if (NotificationId != -1)
                {
                   // _dbManager.UpdateActiveNotificationFlag(NotificationId, false, DateTime.Now);
                }
                //else if (MeterId != -1)
                {
                   // _dbManager.InsertNotification(MeterId, Subject, Body, 2, DateTime.Now, true, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Over loaded Function sendmail with System.web.mail.mailMessage type argument
        /// </summary>
        /// <param name="mailMessage">mailmessage for web mail</param>
        /// <returns>bool value</returns>
        private bool SendMail(System.Web.Mail.MailMessage mailMessage)
        {
            try
            {
                mailMessage.Subject = Subject; //set subject to mailmessage

                if (ValidateEmailAddress(FromAddress))
                {
                    mailMessage.From = FromAddress; //if it not null set from address to mailmessage
                }
                else
                {
                    throw new ArgumentException("Not a valid email address.");
                }

                mailMessage.Body = Body;                                             //set the body content to mailmessage
                mailMessage.BodyFormat = IsHtml ? MailFormat.Html : MailFormat.Text; // check the body type

                //   AlternateView bodyView = AlternateView.CreateAlternateViewFromString(Body, null, "text/Html");

                SetToaddress(mailMessage, null);                                      // set to address to mailmessage
                SetBccAddress(mailMessage, null);                                     //set All bcc address if bcc is added
                SetCcAddress(mailMessage, null);                                      //set all Cc address if Cc is added

                if (Attachments.Count > 0)
                {   //set attachments to mailmessage
                    SetAttachments(mailMessage, null);
                }
                if (ToFlag == 1 && BccFlag == 1 && CcFlag == 1)
                {
                    //if all the flag set to 1 then return false and set a status message
                    throw new ArgumentException("No recipients specified Please Specify any recipients");
                }
                //if flag are not set to 1 then it consist one or more recipients then send the mailmessage using smtpMail.send() and return true
                SmtpMail.Send(mailMessage);
                DeleteAttachmentFiles();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Over loaded Function sendmail with System.net.mail.mailMessage type argument
        /// </summary>
        /// <param name="mailMsg"> mailmessage for sytem.net.mail</param>
        /// <returns>bool value</returns>
        private bool SendMail(MailMessage mailMsg)
        {
            try
            {
                mailMsg.Subject = Subject; //set subject to mailmessage
                mailMsg.Body = Body;       //set body to mailmessage
                if (ValidateEmailAddress(FromAddress))
                {
                    mailMsg.From = new MailAddress(FromAddress); //set from address
                }
                else
                {
                    throw new ArgumentException("Not a valid email address.");
                }
                if (IsHtml)
                    mailMsg.IsBodyHtml = true;         // check body content type
                SetToaddress(null, mailMsg);            // set to address to Mail
                SetBccAddress(null, mailMsg);           //set All bcc address if bcc is added
                SetCcAddress(null, mailMsg);           //set all Cc address if Cc is added
                if (Attachments.Count > 0 || AttachmentFileNames.Count > 0)
                {   // if attachments present the set all attachments to mail message
                    SetAttachments(null, mailMsg);
                }
                if (ToFlag == 1 && BccFlag == 1 && CcFlag == 1)
                {
                    //if all the flag set to 1 then return false and set a status message
                    throw new ApplicationException("No recipients specified Please Specify any recipients");
                }
                //else it consist one or more recipients  
                SmtpClient client = new SmtpClient(ServerName, PortNumber); // provide Smtp server and port number 
                NetworkCredential credential = new NetworkCredential(UserName, Password);//user name and password for authentication
                client.UseDefaultCredentials = false;
                client.Credentials = credential;  //set credentials
                client.EnableSsl = EnableSsl;   //for secure communication enable ssl

                client.Send(mailMsg);   //After authentication send mail and return true
                mailMsg.Dispose();//dispose mail message after sending the mail
                DeleteAttachmentFiles();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// delete all attachments file from server after sending the mail
        /// </summary>
        private void DeleteAttachmentFiles()
        {
            try
            {
                if (_fileArray.Count > 0)// attachments list present
                {
                    foreach (string file in _fileArray)
                    {
                        File.Delete(file);//delete each file
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Send Multiple or Single Attachments
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="mailMsg"></param>
        private void SetAttachments(System.Web.Mail.MailMessage mailMessage, MailMessage mailMsg)
        {
            try
            {
                //Send Multiple Attachments with mail
               /* //if (IsWindowsApplication) // For Window application
                {
                    foreach (string fileName in AttachmentFileNames)
                    {
                        mailMsg.Attachments.Add(new Attachment(fileName));
                    }
                }*/
                //else // for Web application
                {
                    foreach (HttpPostedFile file in Attachments)
                    {
                        int attachmentFileLength = file.ContentLength;
                        if (file != null && file.FileName != "" && attachmentFileLength > 0) //Check File name Exist 
                        {
                            string strFileName = Path.GetFileName(file.FileName);
                            file.SaveAs(HttpContext.Current.Server.MapPath(strFileName));
                            //Check mail is System.net.mail or System.web.mail
                            if (IsSmtpClientMail)
                            {
                                //if it net.mail add multiple attachments to net.mail.mailmessage
                                Attachment attachment = new Attachment(HttpContext.Current.Server.MapPath(strFileName));
                                mailMsg.Attachments.Add(attachment);
                            }
                            else
                            {
                                //web.mail add multiple mailattachments to web.mail.mailmessage
                                MailAttachment attachment = new MailAttachment(HttpContext.Current.Server.MapPath(strFileName));
                                mailMessage.Attachments.Add(attachment);
                            }
                            _fileArray.Add(HttpContext.Current.Server.MapPath(strFileName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///Set Cc Address to the Mail
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="mailMsg"></param>
        private void SetCcAddress(System.Web.Mail.MailMessage mailMessage, MailMessage mailMsg)
        {
            try
            {   //checking ccAddress string is null or empty if it not null then add to mail
                if (!String.IsNullOrEmpty(CcAddress))
                {
                    //split the ccAddress string and add to an array
                    string[] ccArray = CcAddress.Split(";".ToCharArray());
                    ValidateRecipientEmail(ccArray);//validating the email address
                    //if mail is net.mail 
                    if (IsSmtpClientMail)
                    {
                        foreach (string cc in ccArray.Where(cc => !cc.Equals("")))
                        {
                            mailMsg.CC.Add(cc); //add each CcAddress in ccArray to mailmessage.cc
                        }
                    }
                    else
                    {
                        //if mail is web.mail set ccAddress string to MailMessage.Cc
                        CcAddress = CcAddress.TrimEnd(';');
                        mailMessage.Cc = CcAddress;
                    }
                }
                else
                {   //CcAddress is null then set CcFlag to 1
                    CcFlag = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set Bcc Address to the Mail
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="mailMsg"></param>
        private void SetBccAddress(System.Web.Mail.MailMessage mailMessage, MailMessage mailMsg)
        {
            try
            {   //checking bccAddress string is null or empty if it not null then add to mail
                if (!String.IsNullOrEmpty(BccAddress))
                {
                    //split the bccAddress string and add to an array
                    string[] bccArray = BccAddress.Split(";".ToCharArray());
                    ValidateRecipientEmail(bccArray);//validating the email address
                    //if mail is net.mail 
                    if (IsSmtpClientMail)
                    {
                        foreach (string bcc in bccArray.Where(bcc => !bcc.Equals("")))
                        {
                            mailMsg.Bcc.Add(bcc); //add bccAddress array to mailmessage.Bcc
                        }
                    }
                    else
                    {
                        //if mail is web.mail set bccAddress string to MailMessage.Bcc
                        BccAddress = BccAddress.TrimEnd(';');
                        mailMessage.Bcc = BccAddress;
                    }
                }
                else
                {  //bccAddress is null then set BccFlag to 1
                    BccFlag = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set To Address to the Mail
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="mailMsg"></param>
        private void SetToaddress(System.Web.Mail.MailMessage mailMessage, MailMessage mailMsg)
        {
            try
            {   //checking toAddress string is null or empty if it not null then add to mail
                if (!String.IsNullOrEmpty(ToAddress))
                {
                    //split the toAddress string and add to an array
                    string[] toArray = ToAddress.Split(";".ToCharArray());
                    ValidateRecipientEmail(toArray);//validating the email address
                    //if mail is net.mail 
                    if (IsSmtpClientMail)
                    {
                        foreach (string to in toArray.Where(to => !to.Equals("")))
                        {
                            mailMsg.To.Add(to); //add toAddress array to mailmessage.To
                        }
                    }
                    else
                    {
                        //if mail is web.mail set toAddress string to MailMessage.To
                        ToAddress = ToAddress.TrimEnd(';');
                        mailMessage.To = ToAddress; //multiple to address
                    }
                }
                else
                {   //Toaddress is null the set ToFlag=1
                    ToFlag = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //validating the email address
        private static bool ValidateEmailAddress(string emailAddress)
        {
            try
            {
                //string TextToValidate = emailAddress;
                //Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
                //// test email address with expression

                //if (expression.IsMatch(TextToValidate))
                //{
                // is valid email address
                return true;
                //}
                // is not valid email address
                //return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //validating to,bcc,cc email address
        private static void ValidateRecipientEmail(string[] recpArray)
        {
            foreach (string recipient in recpArray.Where(recipient => !recipient.Equals("")))
            {
                if (!ValidateEmailAddress(recipient))
                {
                    throw new ArgumentException(" Not valid email address.");
                }
            }
        }

    }
}
