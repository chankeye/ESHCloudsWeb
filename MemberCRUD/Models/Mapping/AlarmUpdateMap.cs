using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class AlarmUpdateMap : EntityTypeConfiguration<AlarmUpdate>
    {
        public AlarmUpdateMap()
        {
            // Primary Key
            this.HasKey(t => t.UpdateID);

            // Properties
            this.Property(t => t.AlarmID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatePeople)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("AlarmUpdate");
            this.Property(t => t.AlarmID).HasColumnName("AlarmID");
            this.Property(t => t.UpdateID).HasColumnName("UpdateID");
            this.Property(t => t.StartAlarmDate).HasColumnName("StartAlarmDate");
            this.Property(t => t.NextAlarmDate).HasColumnName("NextAlarmDate");
            this.Property(t => t.AlarmCount).HasColumnName("AlarmCount");
            this.Property(t => t.RenewalDueDate).HasColumnName("RenewalDueDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateMemo).HasColumnName("UpdateMemo");
            this.Property(t => t.EstablishDate).HasColumnName("EstablishDate");
            this.Property(t => t.UpdatePeople).HasColumnName("UpdatePeople");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.AlarmMaster)
                .WithMany(t => t.AlarmUpdates)
                .HasForeignKey(d => d.AlarmID);

        }
    }
}
