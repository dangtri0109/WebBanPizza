using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBanPizza.Models;

public partial class PizzaDbContext : DbContext
{
    public PizzaDbContext()
    {
    }

    public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<DanhGia> DanhGia { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DiaChi> DiaChis { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<LichSuTrangThaiDonHang> LichSuTrangThaiDonHangs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TRI\\SQLEXPRESS;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.ChiTietId).HasName("PK__ChiTietD__B117E9CAB5D70CA8");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");

            // ✅ THÊM mapping cho cột GhiChu
            entity.Property(e => e.GhiChu)
                  .HasMaxLength(500)
                  .HasColumnName("GhiChu");

            entity.HasOne(d => d.DonHang).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.DonHangId)
                .HasConstraintName("FK__ChiTietDo__DonHa__6754599E");

            entity.HasOne(d => d.Pizza).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("FK__ChiTietDo__Pizza__68487DD7");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__Coupon__384AF1BA015A581B");

            entity.ToTable("Coupon");

            entity.Property(e => e.Ma).HasMaxLength(20);
            entity.Property(e => e.NgayHetHan).HasColumnType("datetime");
        });

        modelBuilder.Entity<DanhGia>(entity =>
        {
            entity.HasKey(e => e.DanhGiaId).HasName("PK__DanhGia__52C0CA0511CBEFD3");

            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Pizza).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("FK__DanhGia__PizzaId__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DanhGia__UserId__6A30C649");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.DanhMucId).HasName("PK__DanhMuc__1C53A59BF8AD9FF3");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DiaChi>(entity =>
        {
            entity.HasKey(e => e.DiaChiId).HasName("PK__DiaChi__94E668C6B378EECE");

            entity.ToTable("DiaChi");

            entity.Property(e => e.DiaChiChiTiet).HasMaxLength(255);
            entity.Property(e => e.Duong).HasMaxLength(255);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.QuanHuyen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.DiaChis)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DiaChi__UserId__6B24EA82");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.DonHangId).HasName("PK__DonHang__D159F4BEDFDA0F87");

            entity.ToTable("DonHang");

            entity.Property(e => e.DiaChiGiao).HasMaxLength(255);
            entity.Property(e => e.MaKhuyenMai).HasMaxLength(50);
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhiShip).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SdtnguoiNhan)
                .HasMaxLength(20)
                .HasColumnName("SDTNguoiNhan");
            entity.Property(e => e.SoTienGiam).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenNguoiNhan).HasMaxLength(100);
            entity.Property(e => e.TongTien).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xử lý");

            entity.HasOne(d => d.Coupon).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__DonHang__CouponI__6EF57B66");

            entity.HasOne(d => d.DiaChi).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.DiaChiId)
                .HasConstraintName("FK__DonHang__DiaChiI__6D0D32F4");

            entity.HasOne(d => d.PhuongThuc).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.PhuongThucId)
                .HasConstraintName("FK__DonHang__PhuongT__6E01572D");

            entity.HasOne(d => d.Shipper).WithMany(p => p.DonHangShippers)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK_DonHang_Shipper");

            entity.HasOne(d => d.User).WithMany(p => p.DonHangUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DonHang__UserId__6C190EBB");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.GioHangId).HasName("PK__GioHang__4242286DC1E98746");

            entity.ToTable("GioHang");

            entity.HasOne(d => d.Pizza).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("FK__GioHang__PizzaId__71D1E811");

            entity.HasOne(d => d.User).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__GioHang__UserId__70DDC3D8");
        });

        modelBuilder.Entity<LichSuTrangThaiDonHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichSuTr__3214EC072D849E74");

            entity.ToTable("LichSuTrangThaiDonHang");

            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.DonHang).WithMany(p => p.LichSuTrangThaiDonHangs)
                .HasForeignKey(d => d.DonHangId)
                .HasConstraintName("FK__LichSuTra__DonHa__72C60C4A");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__NguoiDun__1788CC4CE6284A7D");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534EA3E693B").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.VaiTro).HasMaxLength(20);
        });

        modelBuilder.Entity<PhuongThucThanhToan>(entity =>
        {
            entity.HasKey(e => e.PhuongThucId).HasName("PK__PhuongTh__1E306C6338AC9263");

            entity.ToTable("PhuongThucThanhToan");

            entity.Property(e => e.TenPhuongThuc).HasMaxLength(100);
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.PizzaId).HasName("PK__Pizza__0B6012DD112732C0");

            entity.ToTable("Pizza");

            entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.KichThuoc).HasMaxLength(10);
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.DanhMuc).WithMany(p => p.Pizzas)
                .HasForeignKey(d => d.DanhMucId)
                .HasConstraintName("FK__Pizza__DanhMucId__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
