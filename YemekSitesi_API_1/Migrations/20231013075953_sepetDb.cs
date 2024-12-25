using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekSitesi_API_1.Migrations
{
    /// <inheritdoc />
    public partial class sepetDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepet_1_Kullanıcılar_userID",
                table: "Sepet_1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sepet_1",
                table: "Sepet_1");

            migrationBuilder.RenameTable(
                name: "Sepet_1",
                newName: "Sepet");

            migrationBuilder.RenameIndex(
                name: "IX_Sepet_1_userID",
                table: "Sepet",
                newName: "IX_Sepet_userID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sepet",
                table: "Sepet",
                column: "sepetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sepet_Kullanıcılar_userID",
                table: "Sepet",
                column: "userID",
                principalTable: "Kullanıcılar",
                principalColumn: "userID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepet_Kullanıcılar_userID",
                table: "Sepet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sepet",
                table: "Sepet");

            migrationBuilder.RenameTable(
                name: "Sepet",
                newName: "Sepet_1");

            migrationBuilder.RenameIndex(
                name: "IX_Sepet_userID",
                table: "Sepet_1",
                newName: "IX_Sepet_1_userID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sepet_1",
                table: "Sepet_1",
                column: "sepetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sepet_1_Kullanıcılar_userID",
                table: "Sepet_1",
                column: "userID",
                principalTable: "Kullanıcılar",
                principalColumn: "userID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
