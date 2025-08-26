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

    public string? SdtnguoiNhan { get; set; } // tên gốc từ DB

    // ✅ Alias để code cũ gọi được SDTNguoiNhan
    [NotMapped]
    public string? SDTNguoiNhan
    {
        get => SdtnguoiNhan;
        set => SdtnguoiNhan = value;
    }

    public decimal PhiShip { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    public virtual Coupon? Coupon { get; set; }
    public virtual DiaChi? DiaChi { get; set; }
    public virtual ICollection<LichSuTrangThaiDonHang> LichSuTrangThaiDonHangs { get; set; } = new List<LichSuTrangThaiDonHang>();
    public virtual PhuongThucThanhToan? PhuongThuc { get; set; }
    public virtual NguoiDung? Shipper { get; set; }
    public virtual NguoiDung? User { get; set; }
}
