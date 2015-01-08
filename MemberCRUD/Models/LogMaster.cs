namespace ESHCloudsWeb.Models
{
    public partial class LogMaster
    {
        public string CN { get; set; }
        public int LogID { get; set; }
        public string ListID { get; set; }
        public string SystemName { get; set; }
        public string ModifyType { get; set; }
        public string ModifyDescription { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifyPeople { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
    }
}
