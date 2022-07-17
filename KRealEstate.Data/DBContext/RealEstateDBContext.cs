using System;
using System.Collections.Generic;
using KRealEstate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KRealEstate.Data.DBContext
{
    public partial class RealEstateDBContext : DbContext
    {
        public RealEstateDBContext()
        {
        }

        public RealEstateDBContext(DbContextOptions<RealEstateDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<AdministrativeRegion> AdministrativeRegions { get; set; } = null!;
        public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Direction> Directions { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostDetail> PostDetails { get; set; } = null!;
        public virtual DbSet<PostType> PostTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductMapCategory> ProductMapCategories { get; set; } = null!;
        public virtual DbSet<ProductVideo> ProductVideos { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<UnitPricePost> UnitPricePosts { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=RealEstateEnv; user id=sa; password=nana01218909214");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictCode).HasMaxLength(20);

                entity.Property(e => e.ProviceCode).HasMaxLength(20);

                entity.Property(e => e.WardCode).HasMaxLength(20);

                entity.HasOne(d => d.DistrictCodeNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dis_add");

                entity.HasOne(d => d.ProviceCodeNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.ProviceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_provice_add");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_region_add");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_unit_add");

                entity.HasOne(d => d.WardCodeNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.WardCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ward_add");
            });

            modelBuilder.Entity<AdministrativeRegion>(entity =>
            {
                entity.ToTable("administrative_regions");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(255)
                    .HasColumnName("code_name");

                entity.Property(e => e.CodeNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("code_name_en");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(255)
                    .HasColumnName("name_en");
            });

            modelBuilder.Entity<AdministrativeUnit>(entity =>
            {
                entity.ToTable("administrative_units");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(255)
                    .HasColumnName("code_name");

                entity.Property(e => e.CodeNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("code_name_en");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.FullNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("full_name_en");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(255)
                    .HasColumnName("short_name");

                entity.Property(e => e.ShortNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("short_name_en");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddressId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(200);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.TaxId).HasMaxLength(200);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_add_user");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Alt).HasMaxLength(255);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameBanner).HasMaxLength(255);

                entity.Property(e => e.Text).HasMaxLength(255);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameCategory).HasMaxLength(255);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.HasIndex(e => e.ProductId, "IX_Contact_ProductId");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddressContact).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameContact).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prod_contact");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("Direction");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("districts_pkey");

                entity.ToTable("districts");

                entity.HasIndex(e => e.AdministrativeUnitId, "IX_districts_administrative_unit_id");

                entity.HasIndex(e => e.ProvinceCode, "IX_districts_province_code");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(255)
                    .HasColumnName("code_name");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.FullNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("full_name_en");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(255)
                    .HasColumnName("name_en");

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(20)
                    .HasColumnName("province_code");

                entity.HasOne(d => d.AdministrativeUnit)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.AdministrativeUnitId)
                    .HasConstraintName("districts_administrative_unit_id_fkey");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceCode)
                    .HasConstraintName("districts_province_code_fkey");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.PostId, "IX_Post_PostId");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DatePost).HasColumnType("date");

                entity.Property(e => e.PostId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.PostNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post_postdetail");
            });

            modelBuilder.Entity<PostDetail>(entity =>
            {
                entity.ToTable("PostDetail");

                entity.HasIndex(e => e.PostTypeId, "IX_PostDetail_PostTypeId");

                entity.HasIndex(e => e.ProductId, "IX_PostDetail_ProductId");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DayPostEnd).HasColumnType("date");

                entity.Property(e => e.DayPostStart).HasColumnType("date");

                entity.Property(e => e.PostTypeId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.PostDetails)
                    .HasForeignKey(d => d.PostTypeId)
                    .HasConstraintName("fk_post_posttype");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PostDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prod_post");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.ToTable("PostType");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NamePostType).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.DirectionId, "IX_Product_DirectionId");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddressDisplay).HasMaxLength(255);

                entity.Property(e => e.AddressId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DirectionId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Furniture).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Project).HasMaxLength(255);

                entity.Property(e => e.Slug)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_add_prod");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DirectionId)
                    .HasConstraintName("fk_direct_prod");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_ProductImages_ProductId");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Alt)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prod_images");
            });

            modelBuilder.Entity<ProductMapCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProductMapCategory");

                entity.HasIndex(e => e.CategoryId, "IX_ProductMapCategory_CategoryId");

                entity.HasIndex(e => e.ProductId, "IX_ProductMapCategory_ProductId");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cate_prodmapcate");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prod_prodmapcate");
            });

            modelBuilder.Entity<ProductVideo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("provinces_pkey");

                entity.ToTable("provinces");

                entity.HasIndex(e => e.AdministrativeRegionId, "IX_provinces_administrative_region_id");

                entity.HasIndex(e => e.AdministrativeUnitId, "IX_provinces_administrative_unit_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.AdministrativeRegionId).HasColumnName("administrative_region_id");

                entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(255)
                    .HasColumnName("code_name");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.FullNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("full_name_en");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(255)
                    .HasColumnName("name_en");

                entity.HasOne(d => d.AdministrativeRegion)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.AdministrativeRegionId)
                    .HasConstraintName("provinces_administrative_region_id_fkey");

                entity.HasOne(d => d.AdministrativeUnit)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.AdministrativeUnitId)
                    .HasConstraintName("provinces_administrative_unit_id_fkey");
            });

            modelBuilder.Entity<UnitPricePost>(entity =>
            {
                entity.ToTable("UnitPricePost");

                entity.HasIndex(e => e.PostTypeId, "IX_UnitPricePost_PostTypeId");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostTypeId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPricePost1)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("UnitPricePost");

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.UnitPricePosts)
                    .HasForeignKey(d => d.PostTypeId)
                    .HasConstraintName("fk_typepost_unitprice");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("wards_pkey");

                entity.ToTable("wards");

                entity.HasIndex(e => e.AdministrativeUnitId, "IX_wards_administrative_unit_id");

                entity.HasIndex(e => e.DistrictCode, "IX_wards_district_code");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(255)
                    .HasColumnName("code_name");

                entity.Property(e => e.DistrictCode)
                    .HasMaxLength(20)
                    .HasColumnName("district_code");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.FullNameEn)
                    .HasMaxLength(255)
                    .HasColumnName("full_name_en");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(255)
                    .HasColumnName("name_en");

                entity.HasOne(d => d.AdministrativeUnit)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.AdministrativeUnitId)
                    .HasConstraintName("wards_administrative_unit_id_fkey");

                entity.HasOne(d => d.DistrictCodeNavigation)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictCode)
                    .HasConstraintName("wards_district_code_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
