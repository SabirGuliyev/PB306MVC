using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaPB306.Migrations
{
    /// <inheritdoc />
    public partial class dropAppUserIdColumnInOrderItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AspNetUsers_AppUserId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_AppUserId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "OrderItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AppUserId",
                table: "OrderItems",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AspNetUsers_AppUserId",
                table: "OrderItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
