using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KRealEstate.Data.Migrations
{
    public partial class AddIdentityFW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProviceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WardCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "administrative_regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrative_regions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "administrative_units",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    short_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    short_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrative_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Organization = table.Column<bool>(type: "bit", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NameBanner = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NameCategory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsShowWeb = table.Column<bool>(type: "bit", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Type = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NamePostType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductVideos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVideos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true),
                    administrative_region_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("provinces_pkey", x => x.code);
                    table.ForeignKey(
                        name: "provinces_administrative_region_id_fkey",
                        column: x => x.administrative_region_id,
                        principalTable: "administrative_regions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "provinces_administrative_unit_id_fkey",
                        column: x => x.administrative_unit_id,
                        principalTable: "administrative_units",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Bedroom = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToletRoom = table.Column<int>(type: "int", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: true),
                    DirectionId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsShowWeb = table.Column<bool>(type: "bit", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressDisplay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Furniture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "fk_direct_prod",
                        column: x => x.DirectionId,
                        principalTable: "Direction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitPricePost",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UnitPricePost = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PostTypeId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPricePost", x => x.Id);
                    table.ForeignKey(
                        name: "fk_typepost_unitprice",
                        column: x => x.PostTypeId,
                        principalTable: "PostType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    province_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("districts_pkey", x => x.code);
                    table.ForeignKey(
                        name: "districts_administrative_unit_id_fkey",
                        column: x => x.administrative_unit_id,
                        principalTable: "administrative_units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "districts_province_code_fkey",
                        column: x => x.province_code,
                        principalTable: "provinces",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NameContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    AddressContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "fk_prod_contact",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DayLengthPost = table.Column<int>(type: "int", nullable: false),
                    DayPostStart = table.Column<DateTime>(type: "date", nullable: true),
                    DayPostEnd = table.Column<DateTime>(type: "date", nullable: true),
                    PostTypeId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDetail", x => x.Id);
                    table.ForeignKey(
                        name: "fk_post_posttype",
                        column: x => x.PostTypeId,
                        principalTable: "PostType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_prod_post",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: true),
                    Alt = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "fk_prod_images",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductMapCategory",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CategoryId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_cate_prodmapcate",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_prod_prodmapcate",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "wards",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    full_name_en = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    code_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    district_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    administrative_unit_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("wards_pkey", x => x.code);
                    table.ForeignKey(
                        name: "wards_administrative_unit_id_fkey",
                        column: x => x.administrative_unit_id,
                        principalTable: "administrative_units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "wards_district_code_fkey",
                        column: x => x.district_code,
                        principalTable: "districts",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PostId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    DatePost = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "fk_post_postdetail",
                        column: x => x.PostId,
                        principalTable: "PostDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ProductId",
                table: "Contact",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_districts_administrative_unit_id",
                table: "districts",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_districts_province_code",
                table: "districts",
                column: "province_code");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostId",
                table: "Post",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDetail_PostTypeId",
                table: "PostDetail",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostDetail_ProductId",
                table: "PostDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DirectionId",
                table: "Product",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMapCategory_CategoryId",
                table: "ProductMapCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMapCategory_ProductId",
                table: "ProductMapCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_administrative_region_id",
                table: "provinces",
                column: "administrative_region_id");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_administrative_unit_id",
                table: "provinces",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPricePost_PostTypeId",
                table: "UnitPricePost",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_wards_administrative_unit_id",
                table: "wards",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_wards_district_code",
                table: "wards",
                column: "district_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductMapCategory");

            migrationBuilder.DropTable(
                name: "ProductVideos");

            migrationBuilder.DropTable(
                name: "UnitPricePost");

            migrationBuilder.DropTable(
                name: "wards");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PostDetail");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "PostType");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropTable(
                name: "administrative_regions");

            migrationBuilder.DropTable(
                name: "administrative_units");
        }
    }
}
