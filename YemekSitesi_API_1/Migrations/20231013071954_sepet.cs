using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekSitesi_API_1.Migrations
{
    /// <inheritdoc />
    public partial class sepet : Migration
    {
        internal static int sepetID;

        public static int productAdet { get; internal set; }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sepet_1",
                columns: table => new
                {
                    sepetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    productAd = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    productAdet = table.Column<int>(type: "int", nullable: false),
                    productImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productFiyat = table.Column<int>(type: "int", nullable: false),
                    productDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepet_1", x => x.sepetID);
                    table.ForeignKey(
                        name: "FK_Sepet_1_Kullanıcılar_userID",
                        column: x => x.userID,
                        principalTable: "Kullanıcılar",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sepet_1_userID",
                table: "Sepet_1",
                column: "userID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sepet_1");
        }
    }
}
