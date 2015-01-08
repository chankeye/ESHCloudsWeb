using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class GroupDetailMap : EntityTypeConfiguration<GroupDetail>
    {
        public GroupDetailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GroupID, t.PeopleID });

            // Properties
            this.Property(t => t.GroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.PeopleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MailType)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("GroupDetail");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.PeopleID).HasColumnName("PeopleID");
            this.Property(t => t.MailType).HasColumnName("MailType");

            // Relationships
            this.HasRequired(t => t.PeopleData)
                .WithMany(t => t.GroupDetails)
                .HasForeignKey(d => d.PeopleID);
            this.HasRequired(t => t.PeopleGroup)
                .WithMany(t => t.GroupDetails)
                .HasForeignKey(d => d.GroupID);

        }
    }
}
