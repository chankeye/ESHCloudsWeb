using System.Data.Entity.ModelConfiguration;

namespace ESHCloudsWeb.Models.Mapping
{
    public class ContentMasterMap : EntityTypeConfiguration<ContentMaster>
    {
        public ContentMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.ContentId);

            // Properties
            this.Property(t => t.CN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Language)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreateUser)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateUser)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContentMaster");
            this.Property(t => t.CN).HasColumnName("CN");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.ShowContent).HasColumnName("ShowContent");
            this.Property(t => t.DisplayStartDate).HasColumnName("DisplayStartDate");
            this.Property(t => t.DisplayEndDate).HasColumnName("DisplayEndDate");
            this.Property(t => t.ShowMarquee).HasColumnName("ShowMarquee");
            this.Property(t => t.MarqueeStartDate).HasColumnName("MarqueeStartDate");
            this.Property(t => t.MarqueeEndDate).HasColumnName("MarqueeEndDate");
            this.Property(t => t.ShowHotIcon).HasColumnName("ShowHotIcon");
            this.Property(t => t.HotIconStartDate).HasColumnName("HotIconStartDate");
            this.Property(t => t.HotIconEndDate).HasColumnName("HotIconEndDate");
            this.Property(t => t.FixTop).HasColumnName("FixTop");
            this.Property(t => t.OrderNumber).HasColumnName("OrderNumber");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.PageView).HasColumnName("PageView");
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");

            // Relationships
            this.HasRequired(t => t.CompanyMaster)
                .WithMany(t => t.ContentMasters)
                .HasForeignKey(d => d.CN);

        }
    }
}
