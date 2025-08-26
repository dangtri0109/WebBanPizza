using System;
using System.Collections.Generic;

namespace WebBanPizza.Models;

public partial class DanhMuc
{
    public int DanhMucId { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
