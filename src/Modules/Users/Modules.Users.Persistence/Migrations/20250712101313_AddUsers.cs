﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Users.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "USERS");

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "USERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    EMAIL = table.Column<string>(type: "text", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "text", nullable: false),
                    LAST_NAME = table.Column<string>(type: "text", nullable: false),
                    CREATED_ON_UTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MODIFIED_ON_UTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL",
                schema: "USERS",
                table: "USERS",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS",
                schema: "USERS");
        }
    }
}
