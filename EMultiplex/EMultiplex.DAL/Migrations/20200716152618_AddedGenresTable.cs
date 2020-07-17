using Microsoft.EntityFrameworkCore.Migrations;

namespace EMultiplex.DAL.Migrations
{
    public partial class AddedGenresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.Sql("Insert Into Genres (Name) Values('Action')");
            migrationBuilder.Sql("Insert Into Genres (Name) Values('Drama')");
            migrationBuilder.Sql("Insert Into Genres (Name) Values('Comedy')");
            migrationBuilder.Sql("Insert Into Genres (Name) Values('Horror')");
            migrationBuilder.Sql("Insert Into Genres (Name) Values('Fiction')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
