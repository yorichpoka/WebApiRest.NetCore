using Microsoft.EntityFrameworkCore;
using MySqlPkg = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Contexts
{
    public class DataBaseMySQLContext : DbContext
    {
        public virtual DbSet<MySqlPkg.Authorization> Authorizations { get; set; }
        public virtual DbSet<MySqlPkg.GroupMenu> GroupMenus { get; set; }
        public virtual DbSet<MySqlPkg.Menu> Menus { get; set; }
        public virtual DbSet<MySqlPkg.Role> Roles { get; set; }
        public virtual DbSet<MySqlPkg.User> Users { get; set; }

        public DataBaseMySQLContext()
        {
        }

        public DataBaseMySQLContext(DbContextOptions<DataBaseMySQLContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MySqlPkg.Authorization>(entity =>
            {
                entity.HasKey(e => new { e.IdMenu, e.IdRole });

                entity.ToTable("_authorization");

                entity.Property(e => e.CreationDate).HasColumnType("DATETIME");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Authorizations)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorization_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Authorizations)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorization_Role");
            });

            modelBuilder.Entity<MySqlPkg.GroupMenu>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("groupmenu");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<MySqlPkg.Menu>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("menu");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.GroupMenu)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.IdGroupMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_GroupMenu");
            });

            modelBuilder.Entity<MySqlPkg.Role>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("Role");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<MySqlPkg.User>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("user");

                entity.HasIndex(e => e.Login)
                    .HasName("IX_User")
                    .IsUnique();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });
        }
    }
}