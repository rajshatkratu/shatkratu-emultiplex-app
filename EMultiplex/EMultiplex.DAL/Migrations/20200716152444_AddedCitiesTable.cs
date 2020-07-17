using Microsoft.EntityFrameworkCore.Migrations;

namespace EMultiplex.DAL.Migrations
{
    public partial class AddedCitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.Sql("Insert Into Cities (Name) values ('Bengaluru')");
            migrationBuilder.Sql("Insert Into Cities (Name) values ('Mumbai')");
            migrationBuilder.Sql("Insert Into Cities (Name) values ('Delhi')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
