using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaMart.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingByteArrayImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageByteArray",
                table: "Photos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageByteArray",
                table: "Photos");

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ImageName", "ProductId" },
                values: new object[] { 3, "PhotoNameTest", 3 });
        }
    }
}
