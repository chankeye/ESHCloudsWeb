namespace ESHCloudsWeb.Models
{
    public partial class GroupDetail
    {
        public int GroupID { get; set; }
        public int PeopleID { get; set; }
        public string MailType { get; set; }
        public virtual PeopleData PeopleData { get; set; }
        public virtual PeopleGroup PeopleGroup { get; set; }
    }
}
