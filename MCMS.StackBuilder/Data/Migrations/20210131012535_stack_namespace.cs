using Microsoft.EntityFrameworkCore.Migrations;

namespace MCMS.StackBuilder.Data.Migrations
{
    public partial class stack_namespace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RootNamespace",
                table: "Stacks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootNamespace",
                table: "Stacks");
        }
    }
}
