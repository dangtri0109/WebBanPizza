using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanPizza.Models;

public partial class DonHang
{
    public int DonHangId { get; set; }
    public int? UserId { get; set; }
    public int? DiaChiId { get; set; }
    public DateTime? NgayDat { get; set; }
    public decimal? TongTien { get; set; }
    public string? TrangThai { get; set; }
    public int? PhuongThucId { get; set; }
    public int? CouponId { get; set; }
    public int? ShipperId { get; set; }
    public string? MaKhuyenMai { get; set; }
    public decimal SoTienGiam { get; set; }
    public bool DaThanhToan { get; set; }
    public string? DiaChiGiao { get; set; }
    public string? TenNguoiNhan { get; set; }
    public string? SdtnguoiNhan { get; set; }
    public decimal PhiShip { get; set; }

    [NotMapped]
    public string? SDTNguoiNhan
    {
        get => SdtnguoiNhan;
        set => SdtnguoiNhan = value;
    }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    public virtual Coupon? Coupon { get; set; }
    public virtual DiaChi? DiaChi { get; set; }
    public virtual ICollection<LichSuTrangThaiDonHang> LichSuTrangThaiDonHangs { get; set; } = new List<LichSuTrangThaiDonHang>();
    public virtual PhuongThucThanhToan? PhuongThuc { get; set; }

    [ForeignKey("UserId")]
    public virtual NguoiDung? User { get; set; }

    [ForeignKey("ShipperId")]
    public virtual NguoiDung? Shipper { get; set; }
}
