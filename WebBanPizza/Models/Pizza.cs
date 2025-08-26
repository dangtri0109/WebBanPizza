using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class Pizza
{
    public int PizzaId { get; set; }

    public string Ten { get; set; } = null!;

    public string? MoTa { get; set; }

    public decimal Gia { get; set; }

    public string? HinhAnh { get; set; }

    public int? DanhMucId { get; set; }

    public string? KichThuoc { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual DanhMuc? DanhMuc { get; set; }

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
