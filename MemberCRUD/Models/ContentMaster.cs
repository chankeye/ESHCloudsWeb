using System;

namespace ESHCloudsWeb.Models
{
    public partial class ContentMaster
    {
        public string CN { get; set; }
        public long ContentId { get; set; }
        public bool ShowContent { get; set; }
        public Nullable<System.DateTime> DisplayStartDate { get; set; }
        public Nullable<System.DateTime> DisplayEndDate { get; set; }
        public Nullable<bool> ShowMarquee { get; set; }
        public Nullable<System.DateTime> MarqueeStartDate { get; set; }
        public Nullable<System.DateTime> MarqueeEndDate { get; set; }
        public Nullable<bool> ShowHotIcon { get; set; }
        public Nullable<System.DateTime> HotIconStartDate { get; set; }
        public Nullable<System.DateTime> HotIconEndDate { get; set; }
        public bool FixTop { get; set; }
        public int OrderNumber { get; set; }
        public int ContentType { get; set; }
        public int PageView { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
    }
}
