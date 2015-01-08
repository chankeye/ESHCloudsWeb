using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class PeopleData
    {
        public PeopleData()
        {
            this.GroupDetails = new List<GroupDetail>();
        }

        public int DepartID { get; set; }
        public int PeopleID { get; set; }
        public string UserID { get; set; }
        public string UserPasd { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public virtual DepartData DepartData { get; set; }
        public virtual ICollection<GroupDetail> GroupDetails { get; set; }
    }
}
