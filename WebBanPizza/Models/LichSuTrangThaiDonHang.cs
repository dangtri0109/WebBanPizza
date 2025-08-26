using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class LichSuTrangThaiDonHang
{
    public int Id { get; set; }

    public int? DonHangId { get; set; }

    public string? TrangThai { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual DonHang? DonHang { get; set; }
}
