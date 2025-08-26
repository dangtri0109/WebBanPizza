using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Web;
using WebBanPizza.Models;

namespace WebBanPizza.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PizzaDbContext _context;
        private readonly IConfiguration _config;

        public PaymentController(PizzaDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // /Payment/Qr/{id}
        public IActionResult Qr(int id)
        {
            var order = _context.DonHangs.Find(id);
            if (order == null) return NotFound();

            var bank = _config["VietQR:BankCode"];
            var accNo = _config["VietQR:AccountNumber"];
            var accName = _config["VietQR:AccountName"];

            // Nội dung chuyển khoản: PIZZA_{orderId}
            var addInfo = $"PIZZA_{order.DonHangId}";
            var encodedInfo = WebUtility.UrlEncode(addInfo);
            var encodedName = WebUtility.UrlEncode(accName ?? "");

            // Ảnh QR của VietQR (img.vietqr.io)
            var qrUrl =
                $"https://img.vietqr.io/image/{bank}-{accNo}-compact.png" +
                $"?amount={(long)order.TongTien}" +   // đồng
                $"&addInfo={encodedInfo}" +
                $"&accountName={encodedName}";

            ViewBag.QrUrl = qrUrl;
            ViewBag.Order = order;
            ViewBag.AddInfo = addInfo;
            ViewBag.Account = $"{accName} - {accNo} ({bank})";

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanDaThanhToan(int id)
        {
            var dh = _context.DonHangs.Find(id);
            if (dh == null) return NotFound();

            dh.DaThanhToan = true;

            // Nếu trước đó vẫn là “Chờ xử lý” bạn có thể đẩy sang “Đang làm”
            if (string.Equals(dh.TrangThai, "Chờ xử lý", StringComparison.OrdinalIgnoreCase))
                dh.TrangThai = "Đang làm";

            _context.SaveChanges();
            TempData["Success"] = $"Đã xác nhận thanh toán đơn #{id}.";
            return RedirectToAction("Dashboard", "QuanLy"); // hoặc trang phù hợp
        }

    }
}
