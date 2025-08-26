using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class PhuongThucThanhToan
{
    public int PhuongThucId { get; set; }

    public string? TenPhuongThuc { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
