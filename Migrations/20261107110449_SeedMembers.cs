using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wt_lab7_7postgres_efcore_talashkevich.Migrations
{
    /// <inheritdoc />
    public partial class SeedMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Age", "CreatedAt", "IsActive", "MembershipPlanId", "Name", "TrainerId" },
                values: new object[,]
                {
                    { 1, 20, new DateTime(2026, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), true, 1, "Анна Петрова", 1 },
                    { 2, 25, new DateTime(2026, 2, 15, 12, 0, 0, 0, DateTimeKind.Utc), true, 2, "Максим Иванов", 2 },
                    { 3, 30, new DateTime(2026, 3, 5, 9, 0, 0, 0, DateTimeKind.Utc), false, 1, "Елена Смирнова", 1 },
                    { 4, 22, new DateTime(2026, 4, 20, 14, 0, 0, 0, DateTimeKind.Utc), true, 3, "София Морозова", 2 },
                    { 5, 35, new DateTime(2026, 5, 12, 11, 0, 0, 0, DateTimeKind.Utc), false, 2, "Дмитрий Козлов", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
