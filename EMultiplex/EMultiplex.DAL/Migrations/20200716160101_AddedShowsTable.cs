using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMultiplex.DAL.Migrations
{
    public partial class AddedShowsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowDate = table.Column<DateTime>(nullable: false),
                    AvailableSeats = table.Column<int>(nullable: false),
                    MaximumSeats = table.Column<int>(nullable: false),
                    PricePerSeat = table.Column<double>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    MultiplexId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shows_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_Multiplexes_MultiplexId",
                        column: x => x.MultiplexId,
                        principalTable: "Multiplexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieId",
                table: "Shows",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MultiplexId",
                table: "Shows",
                column: "MultiplexId");

            migrationBuilder.Sql("INSERT Shows ( [ShowDate], [AvailableSeats], [MaximumSeats], [MovieId], [MultiplexId], [PricePerSeat]) VALUES (CAST(N'2020-07-15T00:00:00.0000000' AS DateTime2), 95, 100, 1, 1, 100)");

            migrationBuilder.Sql("INSERT Shows ( [ShowDate], [AvailableSeats], [MaximumSeats], [MovieId], [MultiplexId], [PricePerSeat]) VALUES (CAST(N'2020-07-16T00:00:00.0000000' AS DateTime2), 100, 100, 2, 2, 150)");
            migrationBuilder.Sql("INSERT Shows ( [ShowDate], [AvailableSeats], [MaximumSeats], [MovieId], [MultiplexId], [PricePerSeat]) VALUES (CAST(N'2020-07-16T00:00:00.0000000' AS DateTime2), 100, 100, 2, 2, 150)");

            migrationBuilder.Sql("INSERT Shows ( [ShowDate], [AvailableSeats], [MaximumSeats], [MovieId], [MultiplexId], [PricePerSeat]) VALUES ( CAST(N'2020-07-15T00:00:00.0000000' AS DateTime2), 10, 100, 2, 1, 200)");

            migrationBuilder.Sql("INSERT Shows ( [ShowDate], [AvailableSeats], [MaximumSeats], [MovieId], [MultiplexId], [PricePerSeat]) VALUES ( CAST(N'2020-05-14T00:00:00.0000000' AS DateTime2), 1, 100, 3, 2, 230)");

            migrationBuilder.Sql("INSERT Shows ( [ShowDate], [AvailableSeats], [MaximumSeats], [MovieId], [MultiplexId], [PricePerSeat]) VALUES ( CAST(N'2020-07-15T00:00:00.0000000' AS DateTime2), 0, 100, 1, 2, 250)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
