using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class DanhGia
{
    public int DanhGiaId { get; set; }

    public int? PizzaId { get; set; }

    public int? UserId { get; set; }

    public int? SoSao { get; set; }

    public string? BinhLuan { get; set; }

    public DateTime? NgayDanhGia { get; set; }

    public virtual Pizza? Pizza { get; set; }

    public virtual NguoiDung? User { get; set; }
}
