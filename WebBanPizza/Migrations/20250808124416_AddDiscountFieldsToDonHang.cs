using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanPizza.Migrations
{
    public partial class AddDiscountFieldsToDonHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaKhuyenMai",
                table: "DonHang",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoTienGiam",
                table: "DonHang",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaKhuyenMai",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "SoTienGiam",
                table: "DonHang");
        }
    }
}
