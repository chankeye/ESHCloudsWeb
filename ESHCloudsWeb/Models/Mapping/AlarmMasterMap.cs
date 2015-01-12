using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class AlarmMasterMap : EntityTypeConfiguration<AlarmMaster>
    {
        public AlarmMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.AlarmID);

            // Properties
            this.Property(t => t.AlarmID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.RenewalFreqUnit)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.EstablishPeople)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AlarmMaster");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.AlarmID).HasColumnName("AlarmID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.RenewalFreq).HasColumnName("RenewalFreq");
            this.Property(t => t.RenewalFreqUnit).HasColumnName("RenewalFreqUnit");
            this.Property(t => t.RenewalDueDate).HasColumnName("RenewalDueDate");
            this.Property(t => t.AlarmDays).HasColumnName("AlarmDays");
            this.Property(t => t.AlarmFreq).HasColumnName("AlarmFreq");
            this.Property(t => t.NextAlarmDate).HasColumnName("NextAlarmDate");
            this.Property(t => t.AlarmToGroupID).HasColumnName("AlarmToGroupID");
            this.Property(t => t.RequireMail).HasColumnName("RequireMail");
            this.Property(t => t.AlarmMemo).HasColumnName("AlarmMemo");
            this.Property(t => t.EstablishDate).HasColumnName("EstablishDate");
            this.Property(t => t.EstablishPeople).HasColumnName("EstablishPeople");
            this.Property(t => t.ItemState).HasColumnName("ItemState");

            // Relationships
            this.HasRequired(t => t.AlarmCategory)
                .WithMany(t => t.AlarmMasters)
                .HasForeignKey(d => d.CategoryID);
            this.HasRequired(t => t.FactoryMaster)
                .WithMany(t => t.AlarmMasters)
                .HasForeignKey(d => d.FactoryID);
            this.HasOptional(t => t.PeopleGroup)
                .WithMany(t => t.AlarmMasters)
                .HasForeignKey(d => d.AlarmToGroupID);

        }
    }
}
