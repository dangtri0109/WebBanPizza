using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanPizza.Migrations
{
    /// <inheritdoc />
    public partial class AddDaThanhToanToDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDo__DonHa__619B8048",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDo__Pizza__628FA481",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DanhGia__PizzaId__6383C8BA",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK__DanhGia__UserId__6477ECF3",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK__DiaChi__UserId__656C112C",
                table: "DiaChi");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__CouponI__693CA210",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__DiaChiI__6754599E",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__PhuongT__68487DD7",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__UserId__66603565",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__GioHang__PizzaId__6C190EBB",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK__GioHang__UserId__6B24EA82",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK__LichSuTra__DonHa__6D0D32F4",
                table: "LichSuTrangThaiDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__Pizza__DanhMucId__6E01572D",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Pizza__0B6012DD9FF9B88B",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK__PhuongTh__1E306C63F29EADFF",
                table: "PhuongThucThanhToan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__NguoiDun__1788CC4C4909B3DF",
                table: "NguoiDung");

            migrationBuilder.DropPrimaryKey(
                name: "PK__LichSuTr__3214EC077FFD39C9",
                table: "LichSuTrangThaiDonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GioHang__4242286DA1D9AFBF",
                table: "GioHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DonHang__D159F4BEA5FA0494",
                table: "DonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DiaChi__94E668C6CCAF40FC",
                table: "DiaChi");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DanhMuc__1C53A59B5C48F364",
                table: "DanhMuc");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DanhGia__52C0CA057D59AF37",
                table: "DanhGia");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Coupon__384AF1BA2EBC8E9A",
                table: "Coupon");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ChiTietD__B117E9CA190068DF",
                table: "ChiTietDonHang");

            migrationBuilder.RenameIndex(
                name: "UQ__NguoiDun__A9D1053463F7C061",
                table: "NguoiDung",
                newName: "UQ__NguoiDun__A9D105349FAAB1B1");

            migrationBuilder.AlterColumn<string>(
                name: "KichThuoc",
                table: "Pizza",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SoTienGiam",
                table: "DonHang",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaKhuyenMai",
                table: "DonHang",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DaThanhToan",
                table: "DonHang",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Pizza__0B6012DDF4BD9403",
                table: "Pizza",
                column: "PizzaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__PhuongTh__1E306C6372D4D71D",
                table: "PhuongThucThanhToan",
                column: "PhuongThucId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__NguoiDun__1788CC4CD3F8AB1B",
                table: "NguoiDung",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__LichSuTr__3214EC07A5BA47C8",
                table: "LichSuTrangThaiDonHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__GioHang__4242286D94FA22C6",
                table: "GioHang",
                column: "GioHangId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DonHang__D159F4BE71CB6806",
                table: "DonHang",
                column: "DonHangId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DiaChi__94E668C6D2B90B3C",
                table: "DiaChi",
                column: "DiaChiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DanhMuc__1C53A59BC6C42238",
                table: "DanhMuc",
                column: "DanhMucId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DanhGia__52C0CA0522F98AD3",
                table: "DanhGia",
                column: "DanhGiaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Coupon__384AF1BA74FCFB76",
                table: "Coupon",
                column: "CouponId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__ChiTietD__B117E9CAEF7626C3",
                table: "ChiTietDonHang",
                column: "ChiTietId");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDo__DonHa__6477ECF3",
                table: "ChiTietDonHang",
                column: "DonHangId",
                principalTable: "DonHang",
                principalColumn: "DonHangId");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDo__Pizza__656C112C",
                table: "ChiTietDonHang",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK__DanhGia__PizzaId__66603565",
                table: "DanhGia",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK__DanhGia__UserId__6754599E",
                table: "DanhGia",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__DiaChi__UserId__68487DD7",
                table: "DiaChi",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__CouponI__6C190EBB",
                table: "DonHang",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__DiaChiI__6A30C649",
                table: "DonHang",
                column: "DiaChiId",
                principalTable: "DiaChi",
                principalColumn: "DiaChiId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__PhuongT__6B24EA82",
                table: "DonHang",
                column: "PhuongThucId",
                principalTable: "PhuongThucThanhToan",
                principalColumn: "PhuongThucId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__UserId__693CA210",
                table: "DonHang",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__GioHang__PizzaId__6EF57B66",
                table: "GioHang",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK__GioHang__UserId__6E01572D",
                table: "GioHang",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__LichSuTra__DonHa__6FE99F9F",
                table: "LichSuTrangThaiDonHang",
                column: "DonHangId",
                principalTable: "DonHang",
                principalColumn: "DonHangId");

            migrationBuilder.AddForeignKey(
                name: "FK__Pizza__DanhMucId__70DDC3D8",
                table: "Pizza",
                column: "DanhMucId",
                principalTable: "DanhMuc",
                principalColumn: "DanhMucId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDo__DonHa__6477ECF3",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietDo__Pizza__656C112C",
                table: "ChiTietDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DanhGia__PizzaId__66603565",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK__DanhGia__UserId__6754599E",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK__DiaChi__UserId__68487DD7",
                table: "DiaChi");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__CouponI__6C190EBB",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__DiaChiI__6A30C649",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__PhuongT__6B24EA82",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__DonHang__UserId__693CA210",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__GioHang__PizzaId__6EF57B66",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK__GioHang__UserId__6E01572D",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK__LichSuTra__DonHa__6FE99F9F",
                table: "LichSuTrangThaiDonHang");

            migrationBuilder.DropForeignKey(
                name: "FK__Pizza__DanhMucId__70DDC3D8",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Pizza__0B6012DDF4BD9403",
                table: "Pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK__PhuongTh__1E306C6372D4D71D",
                table: "PhuongThucThanhToan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__NguoiDun__1788CC4CD3F8AB1B",
                table: "NguoiDung");

            migrationBuilder.DropPrimaryKey(
                name: "PK__LichSuTr__3214EC07A5BA47C8",
                table: "LichSuTrangThaiDonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GioHang__4242286D94FA22C6",
                table: "GioHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DonHang__D159F4BE71CB6806",
                table: "DonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DiaChi__94E668C6D2B90B3C",
                table: "DiaChi");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DanhMuc__1C53A59BC6C42238",
                table: "DanhMuc");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DanhGia__52C0CA0522F98AD3",
                table: "DanhGia");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Coupon__384AF1BA74FCFB76",
                table: "Coupon");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ChiTietD__B117E9CAEF7626C3",
                table: "ChiTietDonHang");

            migrationBuilder.DropColumn(
                name: "DaThanhToan",
                table: "DonHang");

            migrationBuilder.RenameIndex(
                name: "UQ__NguoiDun__A9D105349FAAB1B1",
                table: "NguoiDung",
                newName: "UQ__NguoiDun__A9D1053463F7C061");

            migrationBuilder.AlterColumn<string>(
                name: "KichThuoc",
                table: "Pizza",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SoTienGiam",
                table: "DonHang",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MaKhuyenMai",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Pizza__0B6012DD9FF9B88B",
                table: "Pizza",
                column: "PizzaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__PhuongTh__1E306C63F29EADFF",
                table: "PhuongThucThanhToan",
                column: "PhuongThucId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__NguoiDun__1788CC4C4909B3DF",
                table: "NguoiDung",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__LichSuTr__3214EC077FFD39C9",
                table: "LichSuTrangThaiDonHang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__GioHang__4242286DA1D9AFBF",
                table: "GioHang",
                column: "GioHangId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DonHang__D159F4BEA5FA0494",
                table: "DonHang",
                column: "DonHangId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DiaChi__94E668C6CCAF40FC",
                table: "DiaChi",
                column: "DiaChiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DanhMuc__1C53A59B5C48F364",
                table: "DanhMuc",
                column: "DanhMucId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DanhGia__52C0CA057D59AF37",
                table: "DanhGia",
                column: "DanhGiaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Coupon__384AF1BA2EBC8E9A",
                table: "Coupon",
                column: "CouponId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__ChiTietD__B117E9CA190068DF",
                table: "ChiTietDonHang",
                column: "ChiTietId");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDo__DonHa__619B8048",
                table: "ChiTietDonHang",
                column: "DonHangId",
                principalTable: "DonHang",
                principalColumn: "DonHangId");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDo__Pizza__628FA481",
                table: "ChiTietDonHang",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK__DanhGia__PizzaId__6383C8BA",
                table: "DanhGia",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK__DanhGia__UserId__6477ECF3",
                table: "DanhGia",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__DiaChi__UserId__656C112C",
                table: "DiaChi",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__CouponI__693CA210",
                table: "DonHang",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__DiaChiI__6754599E",
                table: "DonHang",
                column: "DiaChiId",
                principalTable: "DiaChi",
                principalColumn: "DiaChiId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__PhuongT__68487DD7",
                table: "DonHang",
                column: "PhuongThucId",
                principalTable: "PhuongThucThanhToan",
                principalColumn: "PhuongThucId");

            migrationBuilder.AddForeignKey(
                name: "FK__DonHang__UserId__66603565",
                table: "DonHang",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__GioHang__PizzaId__6C190EBB",
                table: "GioHang",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK__GioHang__UserId__6B24EA82",
                table: "GioHang",
                column: "UserId",
                principalTable: "NguoiDung",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__LichSuTra__DonHa__6D0D32F4",
                table: "LichSuTrangThaiDonHang",
                column: "DonHangId",
                principalTable: "DonHang",
                principalColumn: "DonHangId");

            migrationBuilder.AddForeignKey(
                name: "FK__Pizza__DanhMucId__6E01572D",
                table: "Pizza",
                column: "DanhMucId",
                principalTable: "DanhMuc",
                principalColumn: "DanhMucId");
        }
    }
}
