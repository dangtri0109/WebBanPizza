namespace WebBanPizza.Models
{
    public class LichSuDonHangVM
    {
        public int DonHangId { get; set; }
        public DateTime? NgayDat { get; set; }
        public decimal? TongTien { get; set; }   // <-- nullable
        public string TrangThai { get; set; }
        public bool DaThanhToan { get; set; }
        public decimal? PhiShip { get; set; }    // <-- nullable
        public string TenNguoiNhan { get; set; }
        public string SDTNguoiNhan { get; set; }
        public string DiaChiGiao { get; set; }
    }

}
