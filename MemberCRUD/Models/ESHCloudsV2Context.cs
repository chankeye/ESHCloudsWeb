using System.Data.Entity;
using ESHCloudsWeb.Models.Mapping;

namespace ESHCloudsWeb.Models
{
    public partial class ESHCloudsV2Context : DbContext
    {
        static ESHCloudsV2Context()
        {
            Database.SetInitializer<ESHCloudsV2Context>(null);
        }

        public ESHCloudsV2Context()
            : base("Name=ESHCloudsV2Context")
        {
        }

        public DbSet<AlarmCategory> AlarmCategories { get; set; }
        public DbSet<AlarmMaster> AlarmMasters { get; set; }
        public DbSet<AlarmUpdate> AlarmUpdates { get; set; }
        public DbSet<CompanyMaster> CompanyMasters { get; set; }
        public DbSet<ContentMaster> ContentMasters { get; set; }
        public DbSet<DepartData> DepartDatas { get; set; }
        public DbSet<FactoryMaster> FactoryMasters { get; set; }
        public DbSet<FileMaster> FileMasters { get; set; }
        public DbSet<GroupDetail> GroupDetails { get; set; }
        public DbSet<LogMaster> LogMasters { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }
        public DbSet<PeopleData> PeopleDatas { get; set; }
        public DbSet<PeopleGroup> PeopleGroups { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlarmCategoryMap());
            modelBuilder.Configurations.Add(new AlarmMasterMap());
            modelBuilder.Configurations.Add(new AlarmUpdateMap());
            modelBuilder.Configurations.Add(new CompanyMasterMap());
            modelBuilder.Configurations.Add(new ContentMasterMap());
            modelBuilder.Configurations.Add(new DepartDataMap());
            modelBuilder.Configurations.Add(new FactoryMasterMap());
            modelBuilder.Configurations.Add(new FileMasterMap());
            modelBuilder.Configurations.Add(new GroupDetailMap());
            modelBuilder.Configurations.Add(new LogMasterMap());
            modelBuilder.Configurations.Add(new MailLogMap());
            modelBuilder.Configurations.Add(new PeopleDataMap());
            modelBuilder.Configurations.Add(new PeopleGroupMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
