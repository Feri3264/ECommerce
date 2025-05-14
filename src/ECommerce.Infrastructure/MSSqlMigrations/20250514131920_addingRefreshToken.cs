using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.MSSqlMigrations
{
    /// <inheritdoc />
    public partial class addingRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("2a308c81-0485-49a2-8822-a8d61a981093"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Subgroups",
                keyColumn: "Id",
                keyValue: new Guid("d859e766-fb34-4914-9c8d-27b02c40ffd4"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("2a308c81-0485-49a2-8822-a8d61a981093"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Subgroups",
                keyColumn: "Id",
                keyValue: new Guid("d859e766-fb34-4914-9c8d-27b02c40ffd4"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
