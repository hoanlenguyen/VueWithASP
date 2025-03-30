using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIndentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    CreatorUserId = table.Column<int>(type: "int", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ChangedByUser = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "(user_name())")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    UserType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ChangedByUser = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "(user_name())")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SystemStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Identity")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Roles_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Identity_Users_HISTORY")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "Auditing")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SystemEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SystemStart");
        }
    }
}
