using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class PeopleDataMap : EntityTypeConfiguration<PeopleData>
    {
        public PeopleDataMap()
        {
            // Primary Key
            this.HasKey(t => t.PeopleID);

            // Properties
            this.Property(t => t.UserID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserPasd)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Mail)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PeopleData");
            this.Property(t => t.DepartID).HasColumnName("DepartID");
            this.Property(t => t.PeopleID).HasColumnName("PeopleID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UserPasd).HasColumnName("UserPasd");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Mail).HasColumnName("Mail");

            // Relationships
            this.HasRequired(t => t.DepartData)
                .WithMany(t => t.PeopleDatas)
                .HasForeignKey(d => d.DepartID);

        }
    }
}
