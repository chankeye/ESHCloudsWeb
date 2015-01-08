using System;

namespace ESHCloudsWeb.Models
{
    public partial class MailLog
    {
        public string CN { get; set; }
        public long Id { get; set; }
        public Nullable<long> UpdateID { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public string MailSubject { get; set; }
        public string ErrorMessage { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
    }
}
