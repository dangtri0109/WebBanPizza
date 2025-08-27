using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebBanPizza.Models
{
    public class PizzaUploadViewModel
    {
        public int PizzaId { get; set; }

        [Required(ErrorMessage = "Tên pizza là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự.")]
        [Display(Name = "Tên")]
        public string Ten { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự.")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá không hợp lệ.")]
        [Display(Name = "Giá")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
        [Display(Name = "Danh mục")]
        public int DanhMucId { get; set; }

        [Required(ErrorMessage = "Chọn kích thước.")]
        [RegularExpression("S|M|L", ErrorMessage = "Chỉ nhận S, M hoặc L.")]
        [Display(Name = "Kích thước")]
        public string KichThuoc { get; set; } = "M";

        // Dùng khi sửa
        public string HinhAnhCu { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        public IFormFile HinhAnhUpload { get; set; }
    }
}
