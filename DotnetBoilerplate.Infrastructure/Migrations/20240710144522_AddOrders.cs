using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DotnetBoilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_name = table.Column<string>(type: "text", nullable: false),
                    customer_phone = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders_order", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 45, 21, 925, DateTimeKind.Utc).AddTicks(4964), new DateTime(2024, 7, 10, 14, 45, 21, 925, DateTimeKind.Utc).AddTicks(4964) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 45, 21, 925, DateTimeKind.Utc).AddTicks(4966), new DateTime(2024, 7, 10, 14, 45, 21, 925, DateTimeKind.Utc).AddTicks(4966) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9156), new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9183), new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9183), "$2a$11$DZHMDBClWwvEEXmVqUZY/OIJbgoQX/t7eOWdlV6NO/wnXEAerr7dK", new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9186) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9189), new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9189), new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9189), "$2a$11$DZHMDBClWwvEEXmVqUZY/OIJbgoQX/t7eOWdlV6NO/wnXEAerr7dK", new DateTime(2024, 7, 10, 14, 45, 22, 48, DateTimeKind.Utc).AddTicks(9190) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders_order");

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5208), new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5209) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5210), new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5211) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9341), new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9401), new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9402), "$2a$11$AG9aV9QBRiDHauLzwJOfl.ok2TnLDZuwtlVZccywhU1BcCJwYFyfS", new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9409) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9413), new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9413), new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9414), "$2a$11$AG9aV9QBRiDHauLzwJOfl.ok2TnLDZuwtlVZccywhU1BcCJwYFyfS", new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9414) });
        }
    }
}
