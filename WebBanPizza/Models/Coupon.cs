using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string? Ma { get; set; }

    public int? GiamGiaPhanTram { get; set; }

    public DateTime? NgayHetHan { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
