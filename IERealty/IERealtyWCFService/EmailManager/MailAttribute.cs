using System.Collections.Generic;

namespace IERealtyWCFService.EmailManager
{
    public class MailAttribute
    {
        public string FromEmailId { get; set; }
        public string PassWord { get; set; }
        public string ToEmailId { get; set; }
        public List<string> CcList;
        public string Subject { get; set; }
        public string Body { get; set; }
        //public MailFormat MailFormat { get; set; }
        public string AttachmentPath { get; set; }

        public string SmtpServer { get; set; }
        public string SmtpServerPort { get; set; }
    }
}