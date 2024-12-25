using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekSitesi_API_1.Migrations
{
    /// <inheritdoc />
    public partial class sepetDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sepet_1",
                columns: new[] { "sepetID", "productAd", "productAdet", "productDate", "productFiyat", "productImg", "userID" },
                values: new object[] { 1, "Baklava", 1, new DateTime(2023, 10, 13, 10, 36, 55, 0, DateTimeKind.Unspecified), 15, "https://cdn.getiryemek.com/restaurants/1648114161397_1125x522.jpeg", 200 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sepet_1",
                keyColumn: "sepetID",
                keyValue: 1);
        }
    }
}
