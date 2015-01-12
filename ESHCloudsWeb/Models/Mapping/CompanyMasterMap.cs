using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class CompanyMasterMap : EntityTypeConfiguration<CompanyMaster>
    {
        public CompanyMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.CN);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CompanyMaster");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
        }
    }
}
