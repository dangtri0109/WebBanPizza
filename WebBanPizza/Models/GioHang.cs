using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class GioHang
{
    public int GioHangId { get; set; }

    public int? UserId { get; set; }

    public int? PizzaId { get; set; }

    public int SoLuong { get; set; }

    public virtual Pizza? Pizza { get; set; }

    public virtual NguoiDung? User { get; set; }
}
