namespace WebBanPizza.Models
{
    public class PizzaFilterVM
    {
        public string? Keyword { get; set; }
        public int? DanhMucId { get; set; }
        public string? KichThuoc { get; set; } // S|M|L
        public decimal? GiaMin { get; set; }
        public decimal? GiaMax { get; set; }

        public IEnumerable<Pizza> KetQua { get; set; } = new List<Pizza>();
        public IEnumerable<DanhMuc> DanhMucs { get; set; } = new List<DanhMuc>();
    }
}
