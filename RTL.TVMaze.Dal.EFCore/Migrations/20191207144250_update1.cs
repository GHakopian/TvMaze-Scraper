using Microsoft.EntityFrameworkCore.Migrations;

namespace RTL.TVMaze.Dal.EFCore.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "birthday",
                table: "People",
                newName: "Birthday");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "People",
                newName: "birthday");
        }
    }
}
