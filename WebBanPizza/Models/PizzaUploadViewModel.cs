using Microsoft.AspNetCore.Http;

namespace WebBanPizza.Models
{
    public class PizzaUploadViewModel
    {
        public int PizzaId { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public int DanhMucId { get; set; }
        public string? HinhAnhCu { get; set; } // dùng khi sửa
        public IFormFile? HinhAnhUpload { get; set; }
    }
}
