using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetBoilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles_role",
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
                    table.PrimaryKey("PK_roles_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_superuser = table.Column<bool>(type: "boolean", nullable: false),
                    is_staff = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    date_joined = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_user_roles_role_role_id",
                        column: x => x.role_id,
                        principalTable: "roles_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "roles_role",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5208), "admin", new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5209) },
                    { 2, new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5210), "member", new DateTime(2024, 6, 23, 3, 0, 20, 90, DateTimeKind.Utc).AddTicks(5211) }
                });

            migrationBuilder.InsertData(
                table: "users_user",
                columns: new[] { "id", "created_at", "date_joined", "email", "name", "is_active", "is_staff", "is_superuser", "last_login", "password", "role_id", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9341), new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9401), "admin@email.com", "Admin", true, false, true, new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9402), "$2a$11$AG9aV9QBRiDHauLzwJOfl.ok2TnLDZuwtlVZccywhU1BcCJwYFyfS", 1, new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9409) },
                    { 2, new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9413), new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9413), "long@email.com", "Long", true, false, false, new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9414), "$2a$11$AG9aV9QBRiDHauLzwJOfl.ok2TnLDZuwtlVZccywhU1BcCJwYFyfS", 2, new DateTime(2024, 6, 23, 3, 0, 20, 217, DateTimeKind.Utc).AddTicks(9414) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_user_email",
                table: "users_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_role_id",
                table: "users_user",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users_user");

            migrationBuilder.DropTable(
                name: "roles_role");
        }
    }
}
