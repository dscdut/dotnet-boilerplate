using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetBoilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 7, 24, 43, 586, DateTimeKind.Utc).AddTicks(9633), new DateTime(2024, 6, 4, 7, 24, 43, 586, DateTimeKind.Utc).AddTicks(9634) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 7, 24, 43, 586, DateTimeKind.Utc).AddTicks(9635), new DateTime(2024, 6, 4, 7, 24, 43, 586, DateTimeKind.Utc).AddTicks(9636) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5040), new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5075), new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5075), "$2a$11$TASua5/ytWoxMgdleaswm.0ko7z1rZ7VzqobO7zBGgB9hUz1ulDY6", new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5081) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5084), new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5084), new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5085), "$2a$11$TASua5/ytWoxMgdleaswm.0ko7z1rZ7VzqobO7zBGgB9hUz1ulDY6", new DateTime(2024, 6, 4, 7, 24, 43, 720, DateTimeKind.Utc).AddTicks(5085) });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6275), new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6275) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6277), new DateTime(2024, 6, 4, 5, 5, 4, 41, DateTimeKind.Utc).AddTicks(6277) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1902), new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1937), new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1938), "$2a$11$hTpEsotP8MT6sBmBpxVN5epsizoOy2zjGC49eoq/01SPYwmRqbGjS", new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1943), new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1943), new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1944), "$2a$11$hTpEsotP8MT6sBmBpxVN5epsizoOy2zjGC49eoq/01SPYwmRqbGjS", new DateTime(2024, 6, 4, 5, 5, 4, 165, DateTimeKind.Utc).AddTicks(1944) });
        }
    }
}
