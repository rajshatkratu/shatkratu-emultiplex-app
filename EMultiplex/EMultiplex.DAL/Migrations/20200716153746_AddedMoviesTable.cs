using Microsoft.EntityFrameworkCore.Migrations;

namespace EMultiplex.DAL.Migrations
{
    public partial class AddedMoviesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_LanguageId",
                table: "Movies",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Name",
                table: "Movies",
                column: "Name",
                unique: true);

            migrationBuilder.Sql("INSERT Movies ([Name], [GenreId], [LanguageId]) VALUES ( N'Sarkar', 1, 2)");
            migrationBuilder.Sql("INSERT Movies ( [Name], [GenreId], [LanguageId]) VALUES ( N'Bol Bacchan', 3, 2)");
            migrationBuilder.Sql(" INSERT Movies ( [Name], [GenreId], [LanguageId]) VALUES ( N'Shool', 2, 2)");
            migrationBuilder.Sql("INSERT Movies ( [Name], [GenreId], [LanguageId]) VALUES ( N'Prince Of Percia', 2, 1)");
            migrationBuilder.Sql("INSERT Movies ( [Name], [GenreId], [LanguageId]) VALUES ( N'Tengo Charlie', 1, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
