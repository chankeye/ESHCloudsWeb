using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class AlarmCategoryMap : EntityTypeConfiguration<AlarmCategory>
    {
        public AlarmCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryID);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AlarmCategory");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");

            // Relationships
            this.HasRequired(t => t.CompanyMaster)
                .WithMany(t => t.AlarmCategories)
                .HasForeignKey(d => d.CN);

        }
    }
}
