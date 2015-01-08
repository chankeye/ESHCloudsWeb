using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class CompanyMaster
    {
        public CompanyMaster()
        {
            this.AlarmCategories = new List<AlarmCategory>();
            this.ContentMasters = new List<ContentMaster>();
            this.FactoryMasters = new List<FactoryMaster>();
            this.FileMasters = new List<FileMaster>();
            this.LogMasters = new List<LogMaster>();
            this.MailLogs = new List<MailLog>();
        }

        public string CN { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<AlarmCategory> AlarmCategories { get; set; }
        public virtual ICollection<ContentMaster> ContentMasters { get; set; }
        public virtual ICollection<FactoryMaster> FactoryMasters { get; set; }
        public virtual ICollection<FileMaster> FileMasters { get; set; }
        public virtual ICollection<LogMaster> LogMasters { get; set; }
        public virtual ICollection<MailLog> MailLogs { get; set; }
    }
}
