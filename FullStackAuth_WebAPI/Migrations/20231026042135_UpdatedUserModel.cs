using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec329f88-052a-4b97-9d2c-2032e2e5b33a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f920cd91-820b-4ab5-9b1b-58fc076ff1b2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06ea5590-00f3-4164-9c0c-2c43685863d3", null, "Admin", "ADMIN" },
                    { "cf9ca42e-d432-4a27-8a1d-504e7c3da861", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06ea5590-00f3-4164-9c0c-2c43685863d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf9ca42e-d432-4a27-8a1d-504e7c3da861");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ec329f88-052a-4b97-9d2c-2032e2e5b33a", null, "User", "USER" },
                    { "f920cd91-820b-4ab5-9b1b-58fc076ff1b2", null, "Admin", "ADMIN" }
                });
        }
    }
}
