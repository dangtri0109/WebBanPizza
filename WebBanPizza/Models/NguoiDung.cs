﻿using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class NguoiDung
{
    public int UserId { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? MatKhau { get; set; }

    public string? VaiTro { get; set; }

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<DiaChi> DiaChis { get; set; } = new List<DiaChi>();

    public virtual ICollection<DonHang> DonHangShippers { get; set; } = new List<DonHang>();

    public virtual ICollection<DonHang> DonHangUsers { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
