using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuzicStore.Migrations
{
    public partial class UpdateFavoriteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Mood",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moodName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mood", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Audio",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    artistId = table.Column<int>(type: "int", nullable: true),
                    genreId = table.Column<int>(type: "int", nullable: true),
                    moodId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    filename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audio", x => x.id);
                    table.ForeignKey(
                        name: "FK__Audio__artistId__3C69FB99",
                        column: x => x.artistId,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Audio__genreId__3D5E1FD2",
                        column: x => x.genreId,
                        principalTable: "Genre",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Audio__moodId__3E52440B",
                        column: x => x.moodId,
                        principalTable: "Mood",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    buyerId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    purchaseDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    discountId = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK__Order__buyerId__412EB0B6",
                        column: x => x.buyerId,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Order__discountI__4222D4EF",
                        column: x => x.discountId,
                        principalTable: "Discount",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    audioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => new { x.userId, x.audioId });
                    table.ForeignKey(
                        name: "FK__Favorite__audioId__3F466844",
                        column: x => x.audioId,
                        principalTable: "Audio",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Favorite__userId__403A8C7D",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    audioId = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.id);
                    table.ForeignKey(
                        name: "FK__Review__audioId__44FF419A",
                        column: x => x.audioId,
                        principalTable: "Audio",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Review__userId__45F365D3",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    AudioId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__D9B8F28A924D8947", x => new { x.OrderId, x.AudioId });
                    table.ForeignKey(
                        name: "FK__OrderDeta__Audio__440B1D61",
                        column: x => x.AudioId,
                        principalTable: "Audio",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__4316F928",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audio_artistId",
                table: "Audio",
                column: "artistId");

            migrationBuilder.CreateIndex(
                name: "IX_Audio_genreId",
                table: "Audio",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_Audio_moodId",
                table: "Audio",
                column: "moodId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_audioId",
                table: "Favorite",
                column: "audioId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_buyerId",
                table: "Order",
                column: "buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_discountId",
                table: "Order",
                column: "discountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_AudioId",
                table: "OrderDetail",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_audioId",
                table: "Review",
                column: "audioId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_userId",
                table: "Review",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Audio");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Mood");
        }
    }
}
