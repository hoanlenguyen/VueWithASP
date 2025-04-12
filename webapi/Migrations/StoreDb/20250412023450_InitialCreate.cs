using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations.StoreDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Store");

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ChangedByUser = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "(user_name())")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ShortDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    AvatarUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    BrandId = table.Column<int>(type: "int", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ChangedByUser = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "(user_name())")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Store",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Store",
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");

            migrationBuilder.CreateTable(
                name: "ProductTags",
                schema: "Store",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => new { x.ProductId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Store",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "Store",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                schema: "Store",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "Store",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                schema: "Store",
                table: "ProductTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTags",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Store")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Store_Products_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "Store")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Store_ProductCategories_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");
        }
    }
}
