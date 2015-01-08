using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class MailLogMap : EntityTypeConfiguration<MailLog>
    {
        public MailLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(1);

            this.Property(t => t.MailSubject)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("MailLog");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UpdateID).HasColumnName("UpdateID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.MailSubject).HasColumnName("MailSubject");
            this.Property(t => t.ErrorMessage).HasColumnName("ErrorMessage");

            // Relationships
            this.HasRequired(t => t.CompanyMaster)
                .WithMany(t => t.MailLogs)
                .HasForeignKey(d => d.CN);

        }
    }
}
