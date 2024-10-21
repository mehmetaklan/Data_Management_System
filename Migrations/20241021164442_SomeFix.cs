using Microsoft.EntityFrameworkCore.Migrations;

namespace HACKATHON.Migrations
{
    public partial class SomeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "VeriGirisleri",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VeriGirisleri");
        }
    }
}
