using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class PeopleGroupMap : EntityTypeConfiguration<PeopleGroup>
    {
        public PeopleGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.GroupID);

            // Properties
            this.Property(t => t.GroupName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PeopleGroup");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.GroupOrder).HasColumnName("GroupOrder");

            // Relationships
            this.HasRequired(t => t.FactoryMaster)
                .WithMany(t => t.PeopleGroups)
                .HasForeignKey(d => d.FactoryID);

        }
    }
}
