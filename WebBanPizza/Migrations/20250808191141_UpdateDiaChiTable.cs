using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanPizza.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiaChiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__DiaChi__UserId__68487DD7",
                table: "DiaChi");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DiaChi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "DiaChi",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChiChiTiet",
                table: "DiaChi",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "DiaChi",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "DiaChi",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungUserId",
                table: "DiaChi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanHuyen",
                table: "DiaChi",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
                name: "FK__DiaChi__UserId__68487DD7",
                table: "DiaChi",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaChi_NguoiDung_NguoiDungUserId",
                table: "DiaChi");

            migrationBuilder.DropForeignKey(
                name: "FK__DiaChi__UserId__68487DD7",
                table: "DiaChi");

            migrationBuilder.DropIndex(
                name: "IX_DiaChi_NguoiDungUserId",
                table: "DiaChi");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "DiaChi");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "DiaChi");

            migrationBuilder.DropColumn(
                name: "NguoiDungUserId",
                table: "DiaChi");

            migrationBuilder.DropColumn(
                name: "QuanHuyen",
                table: "DiaChi");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DiaChi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "DiaChi",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChiChiTiet",
                table: "DiaChi",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK__DiaChi__UserId__68487DD7",
                table: "DiaChi",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");
        }
    }
}
