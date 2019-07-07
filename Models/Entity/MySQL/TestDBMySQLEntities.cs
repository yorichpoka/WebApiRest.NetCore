﻿using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApiRest.NetCore.Models.Entity;

namespace WebApiRest.NetCore.Models.Entity.MySQL
{
    public class TestDBMySQLEntities : DbContext
    {
        public virtual DbSet<TblAuthorization> Authorizations { get; set; }
        public virtual DbSet<TblGroupMenu> GroupMenus { get; set; }
        public virtual DbSet<TblMenu> Menus { get; set; }
        public virtual DbSet<TblRole> Roles { get; set; }
        public virtual DbSet<TblUser> Users { get; set; }

        public TestDBMySQLEntities() { }

        public TestDBMySQLEntities(DbContextOptions<TestDBMySQLEntities> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseMySQL(
            //        ConfigurationManager.ConnectionStrings["TestDBMySqlEntities"].ConnectionString
            //    );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TblAuthorization>(entity =>
            {
                entity.HasKey(e => new { e.IdMenu, e.IdRole });

                entity.ToTable("_Authorization");

                entity.Property(e => e.CreationDate).HasColumnType("DATETIME");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.C_Authorization)
                    .HasForeignKey(d => d.IdMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorization_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.C_Authorization)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Authorization_Role");
            });

            modelBuilder.Entity<TblGroupMenu>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.GroupMenu)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.IdGroupMenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_GroupMenu");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
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