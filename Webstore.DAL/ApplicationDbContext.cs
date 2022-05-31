using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Webstore.Domain.Entity;

namespace Webstore.DAL
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Order> Order { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProduct { get; set; } = null!;
        public virtual DbSet<Product> Product { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ASUS-ROG-G14\\SSTU2021;Initial Catalog=InfotechMagazine;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK_OrderList");

                entity.Property(e => e.IdOrder)
                    .HasColumnName("ID_order")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdUser).HasColumnName("ID_user");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_UsersList1");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => e.IdOrderProduct)
                    .HasName("PK_OrderProduct");

                entity.Property(e => e.IdOrderProduct)
                    .HasColumnName("ID_orderProduct")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdOrder).HasColumnName("ID_order");

                entity.Property(e => e.IdProduct).HasColumnName("ID_product");

                entity.Property(e => e.PricePerUnit).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_OrderProduct_Order");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {

                entity.HasKey(e => e.Id_product)
                    .HasName("PK_Product");

                entity.Property(e => e.Id_product)
                    .HasColumnName("ID_product")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Img).HasMaxLength(300);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id_user)
                    .HasName("PK_UsersList");

                entity.Property(e => e.Id_user)
                    .HasColumnName("ID_user")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });
            base.OnModelCreating(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
