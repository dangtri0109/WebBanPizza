using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebBanPizza.Models;

namespace WebBanPizza.Controllers
{
    public class DonHangController : Controller
    {
        private readonly PizzaDbContext _context;
        public DonHangController(PizzaDbContext context) => _context = context;

        // GET: /DonHang
        public IActionResult Index()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetString("VaiTro") ?? "User";
            if (uid == null) return RedirectToAction("DangNhap", "Account");

            IQueryable<DonHang> q = _context.DonHangs
                .Include(d => d.User)
                .Include(d => d.DiaChi)
                .OrderByDescending(d => d.NgayDat);

            // Admin xem tất cả; User chỉ xem đơn của mình
            if (!role.Equals("Admin", System.StringComparison.OrdinalIgnoreCase))
                q = q.Where(d => d.UserId == uid.Value);

            var donHangs = q.ToList();

            // Debug tiện kiểm tra (có thể xóa sau)
            ViewBag.Uid = uid;
            ViewBag.Role = role;
            return View(donHangs);
        }

        // GET: /DonHang/LichSuDonHang (nếu vẫn muốn trang riêng cho user)
        [HttpGet]
        public IActionResult LichSuDonHang()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (uid == null) return RedirectToAction("DangNhap", "Account");

            var list = _context.DonHangs
                .Include(x => x.User)
                .Include(x => x.DiaChi)
                .Where(x => x.UserId == uid.Value)
                .OrderByDescending(x => x.NgayDat)
                .ToList();

            return View("LichSuDonHang", list);
        }

        [HttpGet]
        public IActionResult ChiTiet(int id)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetString("VaiTro") ?? "User";
            if (uid == null) return RedirectToAction("DangNhap", "Account");

            var donHang = _context.DonHangs
                .Include(d => d.User)
                .Include(d => d.DiaChi)
                .Include(d => d.ChiTietDonHangs).ThenInclude(c => c.Pizza)
                .FirstOrDefault(d =>
                       d.DonHangId == id &&
                       (role.Equals("Admin", System.StringComparison.OrdinalIgnoreCase) || d.UserId == uid.Value));

            if (donHang == null) return NotFound();
            return View(donHang);
        }
    }
}
