using System.Collections.Generic;

namespace MemberCRUD.DTO
{
    public class PeopleList
    {
        public List<PeopleData> PeopleDataList { get; set; }

        public int Count { get; set; }
    }

    public class PeopleData
    {
        public string CN { get; set; }
        public DepartData Depart { get; set; }
        public string PeopleID { get; set; }
        public string Pasd { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
    }
}