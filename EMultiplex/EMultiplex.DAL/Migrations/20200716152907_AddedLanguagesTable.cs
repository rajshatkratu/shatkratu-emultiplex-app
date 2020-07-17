using Microsoft.EntityFrameworkCore.Migrations;

namespace EMultiplex.DAL.Migrations
{
    public partial class AddedLanguagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.Sql("Insert Into Languages (Name) values ('English')");
            migrationBuilder.Sql("Insert Into Languages (Name) values ('Hindi')");
            migrationBuilder.Sql("Insert Into Languages (Name) values ('Kannada')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
