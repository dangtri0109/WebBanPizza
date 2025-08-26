using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanPizza.Migrations
{
    /// <inheritdoc />
    public partial class AddKhuyenMaiToDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GiamGiaPhanTram = table.Column<int>(type: "int", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Coupon__384AF1BA2EBC8E9A", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    DanhMucId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhMuc__1C53A59B5C48F364", x => x.DanhMucId);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__1788CC4C4909B3DF", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    PhuongThucId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhuongThuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhuongTh__1E306C63F29EADFF", x => x.PhuongThucId);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DanhMucId = table.Column<int>(type: "int", nullable: true),
                    KichThuoc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pizza__0B6012DD9FF9B88B", x => x.PizzaId);
                    table.ForeignKey(
                        name: "FK__Pizza__DanhMucId__6E01572D",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMuc",
                        principalColumn: "DanhMucId");
                });

            migrationBuilder.CreateTable(
                name: "DiaChi",
                columns: table => new
                {
                    DiaChiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DiaChiChiTiet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiaChi__94E668C6CCAF40FC", x => x.DiaChiId);
                    table.ForeignKey(
                        name: "FK__DiaChi__UserId__656C112C",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    DanhGiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SoSao = table.Column<int>(type: "int", nullable: true),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhGia__52C0CA057D59AF37", x => x.DanhGiaId);
                    table.ForeignKey(
                        name: "FK__DanhGia__PizzaId__6383C8BA",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId");
                    table.ForeignKey(
                        name: "FK__DanhGia__UserId__6477ECF3",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    GioHangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PizzaId = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GioHang__4242286DA1D9AFBF", x => x.GioHangId);
                    table.ForeignKey(
                        name: "FK__GioHang__PizzaId__6C190EBB",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId");
                    table.ForeignKey(
                        name: "FK__GioHang__UserId__6B24EA82",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    DonHangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DiaChiId = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TongTien = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Chờ xử lý"),
                    PhuongThucId = table.Column<int>(type: "int", nullable: true),
                    CouponId = table.Column<int>(type: "int", nullable: true),
                    ShipperId = table.Column<int>(type: "int", nullable: true),
                    MaKhuyenMai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoTienGiam = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonHang__D159F4BEA5FA0494", x => x.DonHangId);
                    table.ForeignKey(
                        name: "FK_DonHang_Shipper",
                        column: x => x.ShipperId,
                        principalTable: "NguoiDung",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__DonHang__CouponI__693CA210",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "CouponId");
                    table.ForeignKey(
                        name: "FK__DonHang__DiaChiI__6754599E",
                        column: x => x.DiaChiId,
                        principalTable: "DiaChi",
                        principalColumn: "DiaChiId");
                    table.ForeignKey(
                        name: "FK__DonHang__PhuongT__68487DD7",
                        column: x => x.PhuongThucId,
                        principalTable: "PhuongThucThanhToan",
                        principalColumn: "PhuongThucId");
                    table.ForeignKey(
                        name: "FK__DonHang__UserId__66603565",
                        column: x => x.UserId,
                        principalTable: "NguoiDung",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    ChiTietId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangId = table.Column<int>(type: "int", nullable: true),
                    PizzaId = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__B117E9CA190068DF", x => x.ChiTietId);
                    table.ForeignKey(
                        name: "FK__ChiTietDo__DonHa__619B8048",
                        column: x => x.DonHangId,
                        principalTable: "DonHang",
                        principalColumn: "DonHangId");
                    table.ForeignKey(
                        name: "FK__ChiTietDo__Pizza__628FA481",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId");
                });

            migrationBuilder.CreateTable(
                name: "LichSuTrangThaiDonHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangId = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichSuTr__3214EC077FFD39C9", x => x.Id);
                    table.ForeignKey(
                        name: "FK__LichSuTra__DonHa__6D0D32F4",
                        column: x => x.DonHangId,
                        principalTable: "DonHang",
                        principalColumn: "DonHangId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_DonHangId",
                table: "ChiTietDonHang",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_PizzaId",
                table: "ChiTietDonHang",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_PizzaId",
                table: "DanhGia",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_UserId",
                table: "DanhGia",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_UserId",
                table: "DiaChi",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_CouponId",
                table: "DonHang",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_DiaChiId",
                table: "DonHang",
                column: "DiaChiId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_PhuongThucId",
                table: "DonHang",
                column: "PhuongThucId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ShipperId",
                table: "DonHang",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_UserId",
                table: "DonHang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_PizzaId",
                table: "GioHang",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_UserId",
                table: "GioHang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuTrangThaiDonHang_DonHangId",
                table: "LichSuTrangThaiDonHang",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "UQ__NguoiDun__A9D1053463F7C061",
                table: "NguoiDung",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_DanhMucId",
                table: "Pizza",
                column: "DanhMucId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "LichSuTrangThaiDonHang");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "DiaChi");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
