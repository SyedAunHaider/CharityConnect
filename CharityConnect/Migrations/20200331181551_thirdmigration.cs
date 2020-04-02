using Microsoft.EntityFrameworkCore.Migrations;

namespace CharityConnect.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proivince_City",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_Proivinces_Cities",
                table: "City",
                column: "ProvinceId",
                principalTable: "Proivince",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proivinces_Cities",
                table: "City");

            migrationBuilder.AddForeignKey(
                name: "FK_Proivince_City",
                table: "City",
                column: "ProvinceId",
                principalTable: "Proivince",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
