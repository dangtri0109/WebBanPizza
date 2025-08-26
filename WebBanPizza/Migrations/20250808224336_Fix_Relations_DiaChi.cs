using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanPizza.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Relations_DiaChi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaChi_NguoiDung_NguoiDungUserId",
                table: "DiaChi");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__UserId__693CA210",
                table: "DonHang");

            migrationBuilder.DropIndex(
                name: "IX_DiaChi_NguoiDungUserId",
                table: "DiaChi");

            migrationBuilder.DropColumn(
                name: "NguoiDungUserId",
                table: "DiaChi");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DonHang",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiGiao",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDTNguoiNhan",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNguoiNhan",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__UserId__693CA210",
                table: "DonHang",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__UserId__693CA210",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "DiaChiGiao",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "SDTNguoiNhan",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "TenNguoiNhan",
                table: "DonHang");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DonHang",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungUserId",
                table: "DiaChi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_NguoiDungUserId",
                table: "DiaChi",
                column: "NguoiDungUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaChi_NguoiDung_NguoiDungUserId",
                table: "DiaChi",
                column: "NguoiDungUserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__UserId__693CA210",
                table: "DonHang",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");
        }
    }
}
