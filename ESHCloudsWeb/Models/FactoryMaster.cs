using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class FactoryMaster
    {
        public FactoryMaster()
        {
            this.AlarmMasters = new List<AlarmMaster>();
            this.DepartDatas = new List<DepartData>();
            this.PeopleGroups = new List<PeopleGroup>();
        }

        public string CN { get; set; }
        public int FactoryID { get; set; }
        public string FactoryName { get; set; }
        public virtual ICollection<AlarmMaster> AlarmMasters { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
        public virtual ICollection<DepartData> DepartDatas { get; set; }
        public virtual ICollection<PeopleGroup> PeopleGroups { get; set; }
    }
}
