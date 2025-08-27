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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity => {
            entity.HasKey(e => e.ChiTietId);
            entity.ToTable("ChiTietDonHang");
            entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.HasOne(d => d.DonHang)
                .WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.DonHangId);
            entity.HasOne(d => d.Pizza)
                .WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.PizzaId);
        });

        // ✅ CẤU HÌNH MỐI QUAN HỆ RÕ RÀNG giữa DonHang và NguoiDung (User/Shipper)
        modelBuilder.Entity<DonHang>()
            .HasOne(d => d.User)
            .WithMany(u => u.DonHangUsers)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DonHang>()
            .HasOne(d => d.Shipper)
            .WithMany(u => u.DonHangShippers)
            .HasForeignKey(d => d.ShipperId)
            .OnDelete(DeleteBehavior.Restrict);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
