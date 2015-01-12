using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class PeopleGroup
    {
        public PeopleGroup()
        {
            this.AlarmMasters = new List<AlarmMaster>();
            this.GroupDetails = new List<GroupDetail>();
        }

        public int FactoryID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public virtual ICollection<AlarmMaster> AlarmMasters { get; set; }
        public virtual FactoryMaster FactoryMaster { get; set; }
        public virtual ICollection<GroupDetail> GroupDetails { get; set; }
    }
}
