using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LorKingDom.Models;

public partial class LorKingDomContext : DbContext
{
    public LorKingDomContext()
    {
    }

    public LorKingDomContext(DbContextOptions<LorKingDomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Age> Ages { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Origin> Origins { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PriceRange> PriceRanges { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sex> Sexes { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    public virtual DbSet<StatusOrder> StatusOrders { get; set; }

    public virtual DbSet<SuperCategory> SuperCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=DESKTOP-9N0ESV5\\SQL2022;Database=LorKingDom;User ID=sa;Password=MatKhauMoi_123!;TrustServerCertificate=True");
        => optionsBuilder.UseSqlServer("Server=DESKTOP-74AQ829\\MSSQLSERVER01;Database=LorKingDom;User Id=sa;Password=12345;Encrypt=True;TrustServerCertificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA58659A92462");

            entity.ToTable("Account");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Account__85FB4E3814715170").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D10534B2F18F3B").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountName).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            // entity.Property(e => e.ResetToken).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("Active");
            entity.Property(e => e.TokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Account__RoleID__4222D4EF");
        });

        modelBuilder.Entity<Age>(entity =>
        {
            entity.HasKey(e => e.AgeId).HasName("PK__Age__875454C2D4A8CD57");

            entity.ToTable("Age");

            entity.HasIndex(e => e.AgeRange, "UQ__Age__E0EBEE38C57EC62E").IsUnique();

            entity.Property(e => e.AgeId).HasColumnName("AgeID");
            entity.Property(e => e.AgeRange).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__DAD4F3BE06F890B4");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797E30026F9");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Cart__AccountID__05D8E0BE");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__ProductID__06CD04F7");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2BC5CD8AEA");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Name, "UQ__Category__737584F6AEC97557").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SuperCategoryId).HasColumnName("SuperCategoryID");

            entity.HasOne(d => d.SuperCategory).WithMany(p => p.Categories)
                .HasForeignKey(d => d.SuperCategoryId)
                .HasConstraintName("FK__Category__SuperC__5441852A");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C506131704FC36F4");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E3219CED2E8");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("Unread");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Notificat__Accou__49C3F6B7");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF0582241C");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.ShippingMethodId).HasColumnName("ShippingMethodID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__AccountI__18EBB532");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PaymentM__19DFD96B");

            entity.HasOne(d => d.ShippingMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShippingMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Shipping__1AD3FDA4");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StatusID__1BC821DD");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C13EA7325");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("(([Quantity]*[UnitPrice])*((1)-[Discount]/(100)))", true)
                .HasColumnType("decimal(34, 8)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__245D67DE");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__25518C17");
        });

        modelBuilder.Entity<Origin>(entity =>
        {
            entity.HasKey(e => e.OriginId).HasName("PK__Origin__171FA2C6F2CE4143");

            entity.ToTable("Origin");

            entity.HasIndex(e => e.Name, "UQ__Origin__737584F6BAB90DDC").IsUnique();

            entity.Property(e => e.OriginId).HasColumnName("OriginID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F3578ECC00");

            entity.HasIndex(e => e.MethodName, "UQ__PaymentM__218CFB17F5779E9E").IsUnique();

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.MethodName).HasMaxLength(50);
        });

        modelBuilder.Entity<PriceRange>(entity =>
        {
            entity.HasKey(e => e.PriceRangeId).HasName("PK__PriceRan__B8A301FF1482BD16");

            entity.ToTable("PriceRange");

            entity.Property(e => e.PriceRangeId).HasColumnName("PriceRangeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PriceRange1)
                .HasMaxLength(100)
                .HasColumnName("PriceRange");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6EDE22A1D22");

            entity.ToTable("Product");

            entity.HasIndex(e => e.Sku, "UQ__Product__CA1ECF0D67C8DBA3").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.AgeId).HasColumnName("AgeID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OriginId).HasColumnName("OriginID");
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PriceRangeId).HasColumnName("PriceRangeID");
            entity.Property(e => e.SexId).HasColumnName("SexID");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SKU");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("Available");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Age).WithMany(p => p.Products)
                .HasForeignKey(d => d.AgeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__AgeID__797309D9");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__BrandID__7B5B524B");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__Categor__778AC167");

            entity.HasOne(d => d.Material).WithMany(p => p.Products)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__Materia__787EE5A0");

            entity.HasOne(d => d.Origin).WithMany(p => p.Products)
                .HasForeignKey(d => d.OriginId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__OriginI__7C4F7684");

            entity.HasOne(d => d.PriceRange).WithMany(p => p.Products)
                .HasForeignKey(d => d.PriceRangeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__PriceRa__7D439ABD");

            entity.HasOne(d => d.Sex).WithMany(p => p.Products)
                .HasForeignKey(d => d.SexId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Product__SexID__7A672E12");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__7516F4ECFB533BAA");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIm__Produ__01142BA1");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42F2F067E5144");

            entity.HasIndex(e => e.PromotionCode, "UQ__Promotio__A617E4B63C146B52").IsUnique();

            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.PromotionCode)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Promotion_Product");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEDD01249E");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.ImgReview)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReviewedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Account).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Account__2CF2ADDF");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Product__2DE6D218");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AAD133496");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B61601F481232").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Sex>(entity =>
        {
            entity.HasKey(e => e.SexId).HasName("PK__Sex__75622DB6AEFD4D46");

            entity.ToTable("Sex");

            entity.HasIndex(e => e.Name, "UQ__Sex__737584F653D84CE8").IsUnique();

            entity.Property(e => e.SexId).HasColumnName("SexID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasKey(e => e.ShippingMethodId).HasName("PK__Shipping__0C783384B0C5838B");

            entity.HasIndex(e => e.MethodName, "UQ__Shipping__218CFB17EDA92F2C").IsUnique();

            entity.Property(e => e.ShippingMethodId).HasColumnName("ShippingMethodID");
            entity.Property(e => e.MethodName).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusOrder>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__StatusOr__C8EE20438F2C3BBE");

            entity.ToTable("StatusOrder");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
        });

        modelBuilder.Entity<SuperCategory>(entity =>
        {
            entity.HasKey(e => e.SuperCategoryId).HasName("PK__SuperCat__CEB990D3DF9DE3CE");

            entity.ToTable("SuperCategory");

            entity.HasIndex(e => e.Name, "UQ__SuperCat__737584F60B6D9B9A").IsUnique();

            entity.Property(e => e.SuperCategoryId).HasColumnName("SuperCategoryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
