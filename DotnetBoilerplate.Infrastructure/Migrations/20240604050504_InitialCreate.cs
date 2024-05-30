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
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6275), "Admin", new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6275) },
                    { 2, new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6277), "Member", new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6277) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "date_joined", "email", "full_name", "is_active", "is_staff", "is_superuser", "last_login", "password", "role_id", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1902), new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1937), "admin@email.com", "Admin", true, false, true, new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1938), "$2a$11$hTpEsotP8MT6sBmBpxVN5epsizoOy2zjGC49eoq/01SPYwmRqbGjS", 1, new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1940) },
                    { 2, new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1943), new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1943), "long@email.com", "Long", true, false, false, new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1944), "$2a$11$hTpEsotP8MT6sBmBpxVN5epsizoOy2zjGC49eoq/01SPYwmRqbGjS", 2, new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1944) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
