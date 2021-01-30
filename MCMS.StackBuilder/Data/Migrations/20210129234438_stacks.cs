using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCMS.StackBuilder.Data.Migrations
{
    public partial class stacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PluralName = table.Column<string>(type: "text", nullable: true),
                    PropertiesSerialized = table.Column<string>(type: "text", nullable: true),
                    ConfigSerialized = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stacks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stacks");
        }
    }
}
