using Microsoft.EntityFrameworkCore;
using WebApiRest.NetCore.Repositories.Entities.SqLite;

namespace WebApiRest.NetCore.Repositories.Contexts
{
    public partial class DataBaseSQLiteContext : DbContext
    {
        public virtual DbSet<WebSite> WebSites { get; set; }

        public DataBaseSQLiteContext()
        {
        }

        public DataBaseSQLiteContext(DbContextOptions<DataBaseSQLiteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<WebSite>(entity =>
            {
                entity.HasKey(e => new { e.Key });

                entity.ToTable("web_sites");

                entity.Property(e => e.Title)
                      .HasColumnName("title");

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(e => e.Tags)
                      .HasColumnName("tags");

                entity.Property(e => e.Url)
                      .HasColumnName("url");

                entity.Property(e => e.Author)
                      .HasColumnName("author");

                entity.Property(e => e.Description)
                      .HasColumnName("description");

                entity.Property(e => e.Group)
                      .HasColumnName("group");

                entity.Property(e => e.Place)
                      .HasColumnName("place");

                entity.Property(e => e.Location)
                      .HasColumnName("location");

                entity.Property(e => e.State)
                      .HasColumnName("state");

                entity.Property(e => e.License)
                      .HasColumnName("license");

                entity.Property(e => e.Author_Url)
                      .HasColumnName("author_url");

                entity.Property(e => e.Created)
                      .HasColumnName("created");

                entity.Property(e => e.LastModified)
                      .HasColumnName("lastmodified");

                entity.Property(e => e.MetaDataModified)
                      .HasColumnName("metadatamodified");
            });
        }
    }
}