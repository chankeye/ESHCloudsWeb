using System;
using System.Collections.Generic;

namespace ESHCloudsWeb.Models
{
    public partial class AlarmMaster
    {
        public AlarmMaster()
        {
            this.AlarmUpdates = new List<AlarmUpdate>();
        }

        public int FactoryID { get; set; }
        public string AlarmID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public int RenewalFreq { get; set; }
        public string RenewalFreqUnit { get; set; }
        public System.DateTime RenewalDueDate { get; set; }
        public int AlarmDays { get; set; }
        public int AlarmFreq { get; set; }
        public System.DateTime NextAlarmDate { get; set; }
        public Nullable<int> AlarmToGroupID { get; set; }
        public bool RequireMail { get; set; }
        public string AlarmMemo { get; set; }
        public System.DateTime EstablishDate { get; set; }
        public string EstablishPeople { get; set; }
        public int ItemState { get; set; }
        public virtual AlarmCategory AlarmCategory { get; set; }
        public virtual FactoryMaster FactoryMaster { get; set; }
        public virtual PeopleGroup PeopleGroup { get; set; }
        public virtual ICollection<AlarmUpdate> AlarmUpdates { get; set; }
    }
}
