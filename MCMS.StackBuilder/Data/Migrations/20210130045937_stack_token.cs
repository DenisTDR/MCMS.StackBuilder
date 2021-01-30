using Microsoft.EntityFrameworkCore.Migrations;

namespace MCMS.StackBuilder.Data.Migrations
{
    public partial class stack_token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Stacks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stacks_Token",
                table: "Stacks",
                column: "Token",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stacks_Token",
                table: "Stacks");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Stacks");
        }
    }
}
