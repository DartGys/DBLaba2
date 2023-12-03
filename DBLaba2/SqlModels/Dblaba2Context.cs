using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBLaba2.SqlModels;

public partial class Dblaba2Context : DbContext
{
    public Dblaba2Context()
    {
    }

    public Dblaba2Context(DbContextOptions<Dblaba2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SpecialOffer> SpecialOffers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=boda; Database=DBLaba2; Trusted_Connection=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC0728AF2235");

            entity.ToTable("Inventory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LastStockUpdateDate).HasColumnName("Last_Stock_Update_Date");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.QuantityInStock).HasColumnName("Quantity_In_Stock");
            entity.Property(e => e.StockUpdateDate).HasColumnName("Stock_Update_Date");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Inventory__Produ__3C69FB99");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC076A6751F5");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.LastModifiedDate)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Last_Modified_Date");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_By");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Product_Name");
            entity.Property(e => e.RegistrationDate).HasColumnName("Registration_Date");
        });

        modelBuilder.Entity<SpecialOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpecialO__3214EC0793066510");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LastModifiedDate)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Last_Modified_Date");
            entity.Property(e => e.ModifiedBy).HasColumnName("Modified_By");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.RegistrationDate).HasColumnName("Registration_Date");
            entity.Property(e => e.ValidUntil).HasColumnName("Valid_Until");

            entity.HasOne(d => d.Product).WithMany(p => p.SpecialOffers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SpecialOf__Produ__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
