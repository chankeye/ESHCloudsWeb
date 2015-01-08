namespace ESHCloudsWeb.Models
{
    public partial class FileMaster
    {
        public string CN { get; set; }
        public int FileID { get; set; }
        public string ListID { get; set; }
        public string SystemName { get; set; }
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string EncoedName { get; set; }
        public System.DateTime EstablishDate { get; set; }
        public string EstablishPeople { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
    }
}
