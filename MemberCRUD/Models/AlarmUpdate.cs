using System;

namespace ESHCloudsWeb.Models
{
    public partial class AlarmUpdate
    {
        public string AlarmID { get; set; }
        public int UpdateID { get; set; }
        public System.DateTime StartAlarmDate { get; set; }
        public System.DateTime NextAlarmDate { get; set; }
        public int AlarmCount { get; set; }
        public System.DateTime RenewalDueDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateMemo { get; set; }
        public System.DateTime EstablishDate { get; set; }
        public string UpdatePeople { get; set; }
        public string Status { get; set; }
        public virtual AlarmMaster AlarmMaster { get; set; }
    }
}
