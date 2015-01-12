using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class FileMasterMap : EntityTypeConfiguration<FileMaster>
    {
        public FileMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.FileID);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ListID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SystemName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.EncoedName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(46);

            this.Property(t => t.EstablishPeople)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FileMaster");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.ListID).HasColumnName("ListID");
            this.Property(t => t.SystemName).HasColumnName("SystemName");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.EncoedName).HasColumnName("EncoedName");
            this.Property(t => t.EstablishDate).HasColumnName("EstablishDate");
            this.Property(t => t.EstablishPeople).HasColumnName("EstablishPeople");

            // Relationships
            this.HasRequired(t => t.CompanyMaster)
                .WithMany(t => t.FileMasters)
                .HasForeignKey(d => d.CN);

        }
    }
}
