using Microsoft.EntityFrameworkCore.Migrations;

namespace EMultiplex.DAL.Migrations
{
    public partial class AddedMultiplexesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Multiplexes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multiplexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multiplexes_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Multiplexes_CityId",
                table: "Multiplexes",
                column: "CityId");

            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Gopalan',  1)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Cinepolis',  1)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('PVR Kormangala',  1)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Inox',  1)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Sterling Cineplex',  2)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Metro Inox Cinemas',  2)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Carnival Cinemas Imax Wadla',  2)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Cinepolis Unity one -Rohini',  3)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('PVR Cinemas',  3)");
            migrationBuilder.Sql("Insert Into Multiplexes (Name,  CityId) Values ('Akshara Theatre',  3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Multiplexes");
        }
    }
}
