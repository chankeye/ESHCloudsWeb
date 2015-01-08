using System.Collections.Generic;

namespace ESHCloudsWeb.DTO
{
    public class PeopleList
    {
        public List<PeopleData> PeopleDataList { get; set; }

        public int Count { get; set; }
    }

    public class PeopleData
    {
        public DepartData Depart { get; set; }
        public int PeopleID { get; set; }
        public string UserID { get; set; }
        public string UserPasd { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
    }
}