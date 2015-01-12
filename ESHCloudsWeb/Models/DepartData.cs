using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class DepartData
    {
        public DepartData()
        {
            this.PeopleDatas = new List<PeopleData>();
        }

        public int FactoryID { get; set; }
        public int DepartID { get; set; }
        public string DepartName { get; set; }
        public virtual FactoryMaster FactoryMaster { get; set; }
        public virtual ICollection<PeopleData> PeopleDatas { get; set; }
    }
}
