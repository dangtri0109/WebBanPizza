using System.ComponentModel.DataAnnotations.Schema; // nhớ import

namespace WebBanPizza.Models;

public partial class DiaChi
{
    public int DiaChiId { get; set; }
    public int? UserId { get; set; }
    public string? DiaChiChiTiet { get; set; }

    // Hai cột thật trong DB (giữ nguyên theo DB của bạn)
    public string? SoDienThoai { get; set; }
    public string? Sdt { get; set; }

    public string? HoTen { get; set; }
    public string? Duong { get; set; }
    public string? QuanHuyen { get; set; }
    public string? GhiChu { get; set; }
    public bool MacDinh { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
    public virtual NguoiDung? User { get; set; }

    // ✅ Alias KHÔNG map DB để code cũ gọi dc SDT
    [NotMapped]
    public string? SDT
    {
        get => Sdt ?? SoDienThoai;
        set
        {
            if (Sdt is not null) Sdt = value;
            else SoDienThoai = value;
        }
    }
}
