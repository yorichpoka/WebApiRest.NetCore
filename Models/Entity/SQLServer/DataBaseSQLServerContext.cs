using Microsoft.EntityFrameworkCore;

namespace WebApiRest.NetCore.Models.Entity.SQLServer
{
    public partial class DataBaseSQLServerContext : DbContext
    {
        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<GroupMenu> GroupMenus { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public DataBaseSQLServerContext() { }

        public DataBaseSQLServerContext(DbContextOptions<DataBaseSQLServerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.HasKey(e => new { e.IdRole, e.IdMenu });

                entity.ToTable("_Authorization");

                entity.Property(e => e.CreationDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.Authorization)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorization_Menu");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Authorization)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorization_Role");
            });

            modelBuilder.Entity<GroupMenu>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("GroupMenu");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("Menu");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdGroupMenuNavigation)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.IdGroupMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_GroupMenu");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("Role");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("User");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });
        }
    }
}