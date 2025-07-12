using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Users.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OUTBOX_MESSAGES",
                schema: "USERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OCCURRED_ON_UTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TYPE = table.Column<string>(type: "text", nullable: false),
                    CONTENT = table.Column<string>(type: "json", nullable: false),
                    PROCESSED_ON_UTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ERROR = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OUTBOX_MESSAGES", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OUTBOX_MESSAGES",
                schema: "USERS");
        }
    }
}
