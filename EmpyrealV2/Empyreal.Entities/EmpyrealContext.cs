using Empyreal.Models;
using Empyreal.Models.BaseModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Empyreal.Entities
{
    public class EmpyrealContext : IdentityDbContext<User>
    {
        private readonly DbContextOptions _options;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="DbContext" /> class. The
        ///         <see cref="OnConfiguring(DbContextOptionsBuilder)" />
        ///         method will be called to configure the database (and other options) to be used for this context.
        ///     </para>
        /// </summary>

        #region --- Init ---
        public EmpyrealContext(DbContextOptions options)
            : base(options)
        {
            if (!options.ContextType.GetTypeInfo().IsAssignableFrom(GetType().GetTypeInfo()))
            {
                throw new InvalidOperationException(CoreStrings.NonGenericOptions(GetType().ShortDisplayName()));
            }

            _options = options;

            ServiceProviderCache.Instance.GetOrAdd(options, providerRequired: false)
                .GetRequiredService<IDbSetInitializer>()
                .InitializeSets(this);

        }
        #endregion --- Init ---

        #region --- DbSet ---
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Ward> Ward { get; set; }
        public DbSet<District> District { get; set; }

        #endregion --- DbSet ---

        #region --- Config ---
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Empyreal;Trusted_Connection=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Contents).HasMaxLength(500);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(500);
            });

            //modelBuilder.Entity<AspNetRoleClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId);

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetRoles>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName)
            //        .HasName("RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUserClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogins>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserRoles>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });

            //    entity.HasIndex(e => e.RoleId);

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.RoleId);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserTokens>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sex).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasIndex(e => e.CartId)
                    .HasName("IX_CartDetail_CartId");

                entity.HasIndex(e => e.ProductDetailId)
                    .HasName("IX_CartDetail_ProductId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetail)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_CartDetail_Cart");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .HasConstraintName("FK_CartDetail_ProductDetail");
            });

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateBy).HasMaxLength(450);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasMaxLength(2000);

                entity.Property(e => e.Name).HasMaxLength(2000);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Contents).HasMaxLength(2000);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.LatiLongTude).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Image_Product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasIndex(e => e.OrderId)
                    .HasName("IX_OrderDetail_OrderId");

                entity.HasIndex(e => e.ProductDetailId)
                    .HasName("IX_OrderDetail_ProductId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .HasConstraintName("FK_OrderDetail_ProductDetail");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CatalogId)
                    .HasName("IX_Product_CatalogId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CatalogId).HasColumnName("CatalogID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(2000);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Catalog)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CatalogId)
                    .HasConstraintName("FK_Product_Catalog");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Product_AspNetUsers");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateByUser).HasMaxLength(450);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifyByUser).HasMaxLength(450);

                entity.Property(e => e.LastModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.ColorNavigation)
                    .WithMany(p => p.ProductDetailColorNavigation)
                    .HasForeignKey(d => d.Color)
                    .HasConstraintName("FK_ProductDetail_ProductType");

                entity.HasOne(d => d.PriceNavigation)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.Price)
                    .HasConstraintName("FK_ProductDetail_ProductPrice");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductDetail_Product");

                entity.HasOne(d => d.SizeNavigation)
                    .WithMany(p => p.ProductDetailSizeNavigation)
                    .HasForeignKey(d => d.Size)
                    .HasConstraintName("FK_ProductDetail_ProductType1");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateByUser).HasMaxLength(450);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifyByUser).HasMaxLength(450);

                entity.Property(e => e.LastModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");

                entity.HasOne(d => d.ProductDetailNavigation)
                    .WithMany(p => p.ProductPrices)
                    .HasForeignKey(d => d.ProductDetailId)
                    .HasConstraintName("FK_ProductPrice_ProductDetail");
            });


            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Text).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.CountryCode).HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Type).HasMaxLength(20);

                entity.Property(e => e.ZipCode).HasMaxLength(20);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contents).HasMaxLength(3000);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Tilte).HasMaxLength(2000);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.IsPublished).HasDefaultValueSql("((1))");

                entity.Property(e => e.LatiLongTude).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Ward)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_District");
            });
        }

        #endregion --- Config ---
    }
}
