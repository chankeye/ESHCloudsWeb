using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class DepartDataMap : EntityTypeConfiguration<DepartData>
    {
        public DepartDataMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartID);

            // Properties
            this.Property(t => t.DepartName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DepartData");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.DepartID).HasColumnName("DepartID");
            this.Property(t => t.DepartName).HasColumnName("DepartName");

            // Relationships
            this.HasRequired(t => t.FactoryMaster)
                .WithMany(t => t.DepartDatas)
                .HasForeignKey(d => d.FactoryID);

        }
    }
}
