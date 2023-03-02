using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models.DB;

public partial class ComercializadoraContext : DbContext
{
    public ComercializadoraContext()
    {
    }

    public ComercializadoraContext(DbContextOptions<ComercializadoraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classification> Classifications { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Ordercostumersdetail> Ordercostumersdetails { get; set; }

    public virtual DbSet<Orderscostumer> Orderscostumers { get; set; }

    public virtual DbSet<Packing> Packings { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Typeid> Typeids { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost; Database=Comercializadora; uid=root; pwd=Jumbo2126931..;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("classification");

            entity.Property(e => e.Descripton).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => new { e.TypeIdId, e.TypeIdCodId }, "fk_Customer_TypeId_idx");

            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.IdNumber).HasMaxLength(45);
            entity.Property(e => e.LastNameCustomer).HasMaxLength(45);
            entity.Property(e => e.NameCustomer).HasMaxLength(45);
            entity.Property(e => e.TelNumber).HasMaxLength(45);
            entity.Property(e => e.TypeIdCodId).HasMaxLength(3);

            entity.HasOne(d => d.Type).WithMany(p => p.Customers)
                .HasForeignKey(d => new { d.TypeIdId, d.TypeIdCodId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Customer_TypeId");
        });

        modelBuilder.Entity<Ordercostumersdetail>(entity =>
        {
            entity.HasKey(e => e.IdOrderCostumersDetail).HasName("PRIMARY");

            entity.ToTable("ordercostumersdetail");

            entity.HasIndex(e => e.OrdersCostumersIdOrder, "fk_OrderCostumersDetail_OrdersCostumers1_idx");

            entity.HasIndex(e => e.ProductsIdProducts, "fk_Products_IdProducts_idx");

            entity.Property(e => e.Price).HasMaxLength(45);

            entity.HasOne(d => d.OrdersCostumersIdOrderNavigation).WithMany(p => p.Ordercostumersdetails)
                .HasForeignKey(d => d.OrdersCostumersIdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderCostumersDetail_OrdersCostumers1");

            entity.HasOne(d => d.ProductsIdProductsNavigation).WithMany(p => p.Ordercostumersdetails)
                .HasForeignKey(d => d.ProductsIdProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Products_IdProducts");
        });

        modelBuilder.Entity<Orderscostumer>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("orderscostumers");

            entity.HasIndex(e => e.CustomerId, "fk_OrdersCostumers_Customer1_idx");

            entity.HasIndex(e => e.UsersId, "fk_OrdersCostumers_Users1_idx");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Estado).HasMaxLength(45);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orderscostumers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrdersCostumers_Customer1");

            entity.HasOne(d => d.Users).WithMany(p => p.Orderscostumers)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrdersCostumers_Users1");
        });

        modelBuilder.Entity<Packing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("packing");

            entity.Property(e => e.DescriptionPacking).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProducts).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.ClassificationId, "fk_Products_Classification1_idx");

            entity.HasIndex(e => e.PackingId, "fk_Products_Packing1_idx");

            entity.Property(e => e.Descrption).HasMaxLength(45);

            entity.HasOne(d => d.Classification).WithMany(p => p.Products)
                .HasForeignKey(d => d.ClassificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Products_Classification1");

            entity.HasOne(d => d.Packing).WithMany(p => p.Products)
                .HasForeignKey(d => d.PackingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Products_Packing1");

            entity.HasMany(d => d.Providers).WithMany(p => p.ProductsIdProducts)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsHasProvider",
                    r => r.HasOne<Provider>().WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Products_has_Provider_Provider1"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductsIdProducts")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Products_has_Provider_Products1"),
                    j =>
                    {
                        j.HasKey("ProductsIdProducts", "ProviderId").HasName("PRIMARY");
                        j.ToTable("products_has_provider");
                        j.HasIndex(new[] { "ProductsIdProducts" }, "fk_Products_has_Provider_Products1_idx");
                        j.HasIndex(new[] { "ProviderId" }, "fk_Products_has_Provider_Provider1_idx");
                        j.IndexerProperty<int>("ProductsIdProducts").HasColumnName("Products_IdProducts");
                        j.IndexerProperty<int>("ProviderId").HasColumnName("Provider_Id");
                    });
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("provider");

            entity.HasIndex(e => new { e.TypeIdId, e.TypeIdCodId }, "fk_TypeId_TypeId1_idx");

            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.IdNumber).HasMaxLength(45);
            entity.Property(e => e.LastNameProvider).HasMaxLength(45);
            entity.Property(e => e.NameProvider).HasMaxLength(45);
            entity.Property(e => e.TelNumber).HasMaxLength(45);
            entity.Property(e => e.TypeIdCodId).HasMaxLength(3);

            entity.HasOne(d => d.Type).WithMany(p => p.Providers)
                .HasForeignKey(d => new { d.TypeIdId, d.TypeIdCodId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TypeId_TypeId1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("state");

            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<Typeid>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CodId }).HasName("PRIMARY");

            entity.ToTable("typeid");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CodId).HasMaxLength(3);
            entity.Property(e => e.Description).HasMaxLength(45);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RolesId, "fk_Users_Roles1_idx");

            entity.HasIndex(e => e.StateId, "fk_Users_State1_idx");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(90);
            entity.Property(e => e.Name).HasMaxLength(60);

            entity.HasOne(d => d.Roles).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Users_Roles1");

            entity.HasOne(d => d.State).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Users_State1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
