using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebBanPizza.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ma = table.Column<string>(type: "text", nullable: true),
                    GiamGiaPhanTram = table.Column<int>(type: "integer", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    DanhMucId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenDanhMuc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.DanhMucId);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoTen = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    MatKhau = table.Column<string>(type: "text", nullable: true),
                    VaiTro = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToans",
                columns: table => new
                {
                    PhuongThucId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenPhuongThuc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToans", x => x.PhuongThucId);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ten = table.Column<string>(type: "text", nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    Gia = table.Column<decimal>(type: "numeric", nullable: false),
                    HinhAnh = table.Column<string>(type: "text", nullable: true),
                    DanhMucId = table.Column<int>(type: "integer", nullable: true),
                    KichThuoc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.PizzaId);
                    table.ForeignKey(
                        name: "FK_Pizzas_DanhMucs_DanhMucId",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMucs",
                        principalColumn: "DanhMucId");
                });

            migrationBuilder.CreateTable(
                name: "DiaChis",
                columns: table => new
                {
                    DiaChiId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    DiaChiChiTiet = table.Column<string>(type: "text", nullable: true),
                    SoDienThoai = table.Column<string>(type: "text", nullable: true),
                    Sdt = table.Column<string>(type: "text", nullable: true),
                    HoTen = table.Column<string>(type: "text", nullable: true),
                    Duong = table.Column<string>(type: "text", nullable: true),
                    QuanHuyen = table.Column<string>(type: "text", nullable: true),
                    GhiChu = table.Column<string>(type: "text", nullable: true),
                    MacDinh = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChis", x => x.DiaChiId);
                    table.ForeignKey(
                        name: "FK_DiaChis_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    DanhGiaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PizzaId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    SoSao = table.Column<int>(type: "integer", nullable: true),
                    BinhLuan = table.Column<string>(type: "text", nullable: true),
                    NgayDanhGia = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.DanhGiaId);
                    table.ForeignKey(
                        name: "FK_DanhGia_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DanhGia_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "PizzaId");
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    GioHangId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    PizzaId = table.Column<int>(type: "integer", nullable: true),
                    SoLuong = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.GioHangId);
                    table.ForeignKey(
                        name: "FK_GioHangs_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_GioHangs_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "PizzaId");
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonHangId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    DiaChiId = table.Column<int>(type: "integer", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TongTien = table.Column<decimal>(type: "numeric", nullable: true),
                    TrangThai = table.Column<string>(type: "text", nullable: true),
                    PhuongThucId = table.Column<int>(type: "integer", nullable: true),
                    CouponId = table.Column<int>(type: "integer", nullable: true),
                    ShipperId = table.Column<int>(type: "integer", nullable: true),
                    MaKhuyenMai = table.Column<string>(type: "text", nullable: true),
                    SoTienGiam = table.Column<decimal>(type: "numeric", nullable: false),
                    DaThanhToan = table.Column<bool>(type: "boolean", nullable: false),
                    DiaChiGiao = table.Column<string>(type: "text", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "text", nullable: true),
                    SdtnguoiNhan = table.Column<string>(type: "text", nullable: true),
                    PhiShip = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonHangId);
                    table.ForeignKey(
                        name: "FK_DonHangs_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "CouponId");
                    table.ForeignKey(
                        name: "FK_DonHangs_DiaChis_DiaChiId",
                        column: x => x.DiaChiId,
                        principalTable: "DiaChis",
                        principalColumn: "DiaChiId");
                    table.ForeignKey(
                        name: "FK_DonHangs_NguoiDungs_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonHangs_NguoiDungs_UserId",
                        column: x => x.UserId,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonHangs_PhuongThucThanhToans_PhuongThucId",
                        column: x => x.PhuongThucId,
                        principalTable: "PhuongThucThanhToans",
                        principalColumn: "PhuongThucId");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    ChiTietId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DonHangId = table.Column<int>(type: "integer", nullable: true),
                    PizzaId = table.Column<int>(type: "integer", nullable: true),
                    SoLuong = table.Column<int>(type: "integer", nullable: false),
                    Gia = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.ChiTietId);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHangs_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DonHangs",
                        principalColumn: "DonHangId");
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "PizzaId");
                });

            migrationBuilder.CreateTable(
                name: "LichSuTrangThaiDonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DonHangId = table.Column<int>(type: "integer", nullable: true),
                    TrangThai = table.Column<string>(type: "text", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuTrangThaiDonHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuTrangThaiDonHangs_DonHangs_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DonHangs",
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
                name: "IX_DiaChis_UserId",
                table: "DiaChis",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_CouponId",
                table: "DonHangs",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_DiaChiId",
                table: "DonHangs",
                column: "DiaChiId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_PhuongThucId",
                table: "DonHangs",
                column: "PhuongThucId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_ShipperId",
                table: "DonHangs",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_UserId",
                table: "DonHangs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_PizzaId",
                table: "GioHangs",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_UserId",
                table: "GioHangs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuTrangThaiDonHangs_DonHangId",
                table: "LichSuTrangThaiDonHangs",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_DanhMucId",
                table: "Pizzas",
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
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "LichSuTrangThaiDonHangs");

            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "DiaChis");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToans");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
