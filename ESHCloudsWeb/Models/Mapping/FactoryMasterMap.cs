using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class FactoryMasterMap : EntityTypeConfiguration<FactoryMaster>
    {
        public FactoryMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.FactoryID);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FactoryName)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("FactoryMaster");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.FactoryName).HasColumnName("FactoryName");

            // Relationships
            this.HasRequired(t => t.CompanyMaster)
                .WithMany(t => t.FactoryMasters)
                .HasForeignKey(d => d.CN);

        }
    }
}
