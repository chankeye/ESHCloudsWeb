using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class AlarmCategory
    {
        public AlarmCategory()
        {
            this.AlarmMasters = new List<AlarmMaster>();
        }

        public string CN { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
        public virtual ICollection<AlarmMaster> AlarmMasters { get; set; }
    }
}
