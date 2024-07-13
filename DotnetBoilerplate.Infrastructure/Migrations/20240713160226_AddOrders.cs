using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetBoilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_name = table.Column<string>(type: "text", nullable: false),
                    customer_phone = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    payment_method_id = table.Column<int>(type: "integer", nullable: false),
                    payment_order_id = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2437), "MoMo", new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2438) },
                    { 2, new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2439), "VNPay", new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2440) }
                });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 13, 16, 2, 25, 688, DateTimeKind.Utc).AddTicks(8908), new DateTime(2024, 7, 13, 16, 2, 25, 688, DateTimeKind.Utc).AddTicks(8908) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 13, 16, 2, 25, 688, DateTimeKind.Utc).AddTicks(8916), new DateTime(2024, 7, 13, 16, 2, 25, 688, DateTimeKind.Utc).AddTicks(8917) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2423), new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2423), new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2424), "$2a$11$3cE02Y7wJ1Zxmjv28t6HEuYsWIuIlvEcgpJRoo/pd0K05kEFymrLG", new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2430) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2433), new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2433), new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2433), "$2a$11$3cE02Y7wJ1Zxmjv28t6HEuYsWIuIlvEcgpJRoo/pd0K05kEFymrLG", new DateTime(2024, 7, 13, 16, 2, 25, 825, DateTimeKind.Utc).AddTicks(2434) });

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_method_id",
                table: "orders",
                column: "payment_method_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "payment_methods");

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
