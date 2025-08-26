using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class ChiTietDonHang
{
    public int ChiTietId { get; set; }

    public int? DonHangId { get; set; }

    public int? PizzaId { get; set; }

    public int SoLuong { get; set; }

    public decimal Gia { get; set; }

    public virtual DonHang? DonHang { get; set; }

    public virtual Pizza? Pizza { get; set; }

    public string? GhiChu { get; set; }

}
