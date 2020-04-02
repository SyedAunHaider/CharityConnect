using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CharityConnect.Migrations
{
    public partial class fifthone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 1, 11, 18, 2, 447, DateTimeKind.Local).AddTicks(1695),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2020, 3, 31, 23, 35, 11, 920, DateTimeKind.Local).AddTicks(7492));

            migrationBuilder.AddColumn<int>(
                name: "PConstituencyId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CharityHistory",
                columns: table => new
                {
                    HistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DonorUserId = table.Column<int>(nullable: false),
                    RecipientUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityHistory", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "NAConstituency",
                columns: table => new
                {
                    NAConstituencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NAConstituency", x => x.NAConstituencyId);
                    table.ForeignKey(
                        name: "FK_Cities_NAConstituencies",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvincialConstituency",
                columns: table => new
                {
                    PConstituencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    NAConstituencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialConstituency", x => x.PConstituencyId);
                    table.ForeignKey(
                        name: "FK_NAConstituencies_ProvincialConstituencies",
                        column: x => x.NAConstituencyId,
                        principalTable: "NAConstituency",
                        principalColumn: "NAConstituencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PConstituencyId",
                table: "AspNetUsers",
                column: "PConstituencyId");

            migrationBuilder.CreateIndex(
                name: "IX_NAConstituency_CityId",
                table: "NAConstituency",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialConstituency_NAConstituencyId",
                table: "ProvincialConstituency",
                column: "NAConstituencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvincialConstituencies_AppUsers",
                table: "AspNetUsers",
                column: "PConstituencyId",
                principalTable: "ProvincialConstituency",
                principalColumn: "PConstituencyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvincialConstituencies_AppUsers",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CharityHistory");

            migrationBuilder.DropTable(
                name: "ProvincialConstituency");

            migrationBuilder.DropTable(
                name: "NAConstituency");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PConstituencyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PConstituencyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 31, 23, 35, 11, 920, DateTimeKind.Local).AddTicks(7492),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2020, 4, 1, 11, 18, 2, 447, DateTimeKind.Local).AddTicks(1695));
        }
    }
}
