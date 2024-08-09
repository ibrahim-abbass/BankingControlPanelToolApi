using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCPT.ABSTACTION.Migrations
{
    public partial class AddHistoriesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b7de5fb-be91-4624-b0a2-c76f9ac577e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d57d910-2be6-491f-9c66-2f908fcada6d");

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SearchProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84783f01-8925-4702-8311-60e183defef7", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87ce0f5a-b05f-4c26-b9d7-74cbcb9d9152", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84783f01-8925-4702-8311-60e183defef7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87ce0f5a-b05f-4c26-b9d7-74cbcb9d9152");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b7de5fb-be91-4624-b0a2-c76f9ac577e9", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d57d910-2be6-491f-9c66-2f908fcada6d", "2", "User", "User" });
        }
    }
}
