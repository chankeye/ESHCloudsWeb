using System.Collections.Generic;

namespace ESHCloudsWeb.DTO
{
    public class PeopleGroupList
    {
        public List<PeopleGroup> PeopleGroupDataList { get; set; }

        public int Count { get; set; }
    }

    public class PeopleGroup
    {
        public string FactoryName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public List<PeopleGroupDetail> GroupPeopleList { get; set; }
    }
}