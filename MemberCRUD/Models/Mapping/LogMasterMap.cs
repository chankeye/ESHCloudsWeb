using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class LogMasterMap : EntityTypeConfiguration<LogMaster>
    {
        public LogMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ListID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SystemName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModifyType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModifyDescription)
                .IsRequired();

            this.Property(t => t.ModifyPeople)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LogMaster");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.ListID).HasColumnName("ListID");
            this.Property(t => t.SystemName).HasColumnName("SystemName");
            this.Property(t => t.ModifyType).HasColumnName("ModifyType");
            this.Property(t => t.ModifyDescription).HasColumnName("ModifyDescription");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.ModifyPeople).HasColumnName("ModifyPeople");

            // Relationships
            this.HasRequired(t => t.CompanyMaster)
                .WithMany(t => t.LogMasters)
                .HasForeignKey(d => d.CN);

        }
    }
}
