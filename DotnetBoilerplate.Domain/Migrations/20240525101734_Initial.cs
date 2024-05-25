using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetBoilerplate.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    name = table.Column<string>(type: "text", nullable: true),
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
                    name = table.Column<string>(type: "text", nullable: true),
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
                    { 1, new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(467), "Admin", new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(468) },
                    { 2, new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(469), "Member", new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(470) }
                });

            migrationBuilder.InsertData(
                table: "users_user",
                columns: new[] { "id", "created_at", "date_joined", "email", "is_active", "is_staff", "is_superuser", "last_login", "name", "password", "role_id", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5856), new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5856), "admin@email.com", true, false, true, new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5857), "Admin", "$2a$11$pZHX2y7QdIXaEr.CLm6h/ObJegBir0rYQE5Jzm/MALyiwqzb11Uti", 1, new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5860) },
                    { 2, new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5892), new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5893), "long@email.com", true, false, false, new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5893), "Long", "$2a$11$pZHX2y7QdIXaEr.CLm6h/ObJegBir0rYQE5Jzm/MALyiwqzb11Uti", 2, new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5894) }
                });

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
