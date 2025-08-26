using Microsoft.AspNetCore.Mvc;
using WebBanPizza.Models;
using System.Linq;
using Newtonsoft.Json;

namespace WebBanPizza.Controllers
{
    public class AccountController : Controller
    {
        private readonly PizzaDbContext _context;

        public AccountController(PizzaDbContext context)
        {
            _context = context;
        }

        // GET: /Account/DangKy
        public IActionResult DangKy() => View();

        [HttpPost]
        public IActionResult DangKy(NguoiDung user)
        {
            if (ModelState.IsValid)
            {
                var exist = _context.NguoiDungs.FirstOrDefault(u => u.Email == user.Email);
                if (exist != null)
                {
                    ViewBag.Error = "Email đã được sử dụng.";
                    return View();
                }

                user.VaiTro = "User";
                _context.NguoiDungs.Add(user);
                _context.SaveChanges();

                TempData["Success"] = "Đăng ký thành công. Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap");
            }

            return View();
        }

        // GET: /Account/DangNhap
        public IActionResult DangNhap(string email, string matkhau)
        {
            var user = _context.NguoiDungs
                .FirstOrDefault(u => u.Email == email && u.MatKhau == matkhau);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("HoTen", user.HoTen ?? "");
                HttpContext.Session.SetString("VaiTro", user.VaiTro ?? "");

                if (user.VaiTro == "Admin")
                    return RedirectToAction("Dashboard", "QuanLy");

                return RedirectToAction("Index", "Home"); // User thông thường
            }

            ViewBag.ThongBao = "Sai thông tin đăng nhập!";
            return View();
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}
