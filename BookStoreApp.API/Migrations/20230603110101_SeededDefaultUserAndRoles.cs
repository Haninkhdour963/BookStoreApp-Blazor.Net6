using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03a18a35-bfc1-4ed0-97ad-2e6e35d1dae4", "ce3d47bd-183d-4e3c-a920-b49e27dfc19c", "User", "USER" },
                    { "1ce7e2e7-e88d-4178-abc6-17cfd5422dcc", "17dd7b88-2e65-4f4f-a6d2-c5a9988b125f", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6a934c83-d9d5-4d85-9267-d53e7fb62f8f", 0, "c4d810ad-554e-43ab-9783-2dfe1c107bb1", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEJnvuvxatRGfOrO/DwjFeigNLePZtnBVD4F+rgszUagEUHEBkL7Ay7dimyUEbn6efQ==", null, false, "344d607c-3949-4226-b903-5078574dd178", false, "user@bookstore.com" },
                    { "f6214ff4-b714-4f90-bc99-15885f55aedb", 0, "05852325-63dd-418d-99c9-15942878effe", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEIW6VCIFTfkU4LWnMf7o5jApP+x2bJWeh0pY7unRQGof2c1f30Y6J6S4uNR4vGtiOQ==", null, false, "2455f3bb-840e-4b50-8f47-2794a312269a", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "03a18a35-bfc1-4ed0-97ad-2e6e35d1dae4", "6a934c83-d9d5-4d85-9267-d53e7fb62f8f" },
                    { "1ce7e2e7-e88d-4178-abc6-17cfd5422dcc", "f6214ff4-b714-4f90-bc99-15885f55aedb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "03a18a35-bfc1-4ed0-97ad-2e6e35d1dae4", "6a934c83-d9d5-4d85-9267-d53e7fb62f8f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1ce7e2e7-e88d-4178-abc6-17cfd5422dcc", "f6214ff4-b714-4f90-bc99-15885f55aedb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03a18a35-bfc1-4ed0-97ad-2e6e35d1dae4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ce7e2e7-e88d-4178-abc6-17cfd5422dcc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a934c83-d9d5-4d85-9267-d53e7fb62f8f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6214ff4-b714-4f90-bc99-15885f55aedb");
        }
    }
}
