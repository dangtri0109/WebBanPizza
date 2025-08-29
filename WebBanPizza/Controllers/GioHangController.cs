using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebBanPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanPizza.Controllers
{
    public class GioHangController : Controller
    {
        private readonly PizzaDbContext _context;
        public GioHangController(PizzaDbContext context) => _context = context;

        // ===== helpers =====
        private List<CartItem> LayGioHang()
        {
            var session = HttpContext.Session.GetString("GioHang");
            return string.IsNullOrEmpty(session)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(session) ?? new List<CartItem>();
        }

        private void LuuGioHang(List<CartItem> cart)
        {
            HttpContext.Session.SetString("GioHang", JsonConvert.SerializeObject(cart));
        }

        // ===== views =====
        public IActionResult Index()
        {
            var cart = LayGioHang();

            // ✅ NEW: tổng số lượng sản phẩm trong giỏ (cộng dồn)
            ViewBag.TongSoLuong = cart.Sum(x => x.SoLuong);

            // (tuỳ chọn) tổng số mặt hàng khác nhau
            ViewBag.SoMatHang = cart.Count;

            ViewBag.MaGiamGia = HttpContext.Session.GetString("MaGiamGia");
            ViewBag.GiamGiaPhanTram = HttpContext.Session.GetInt32("GiamGiaPhanTram") ?? 0;
            return View(cart);
        }

        // ===== add to cart (từ trang chi tiết) → nhận đúng soLuong =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemVaoGio(int id, int soLuong = 1, string? returnUrl = null)
        {
            var qty = Math.Max(1, soLuong);             // đảm bảo >= 1

            var pizza = _context.Pizzas.FirstOrDefault(p => p.PizzaId == id);
            if (pizza == null) return NotFound();

            var cart = LayGioHang();
            var item = cart.FirstOrDefault(p => p.Pizza.PizzaId == id);

            if (item != null)
                item.SoLuong += qty;                    // cộng dồn chính xác
            else
                cart.Add(new CartItem { Pizza = pizza, SoLuong = qty });

            LuuGioHang(cart);
            TempData["Success"] = $"✔ Đã thêm {qty} x {pizza.Ten} vào giỏ!";

            return Redirect(returnUrl ?? Url.Action("Index", "Pizza"));
        }

        public IActionResult XoaKhoiGio(int id)
        {
            var cart = LayGioHang();
            var item = cart.FirstOrDefault(x => x.Pizza.PizzaId == id);
            if (item != null)
            {
                cart.Remove(item);
                LuuGioHang(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatSoLuong(int id, int soLuong)
        {
            var cart = LayGioHang();
            var item = cart.FirstOrDefault(x => x.Pizza.PizzaId == id);
            if (item != null)
            {
                item.SoLuong = Math.Max(1, soLuong);
                LuuGioHang(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApDungMaGiamGia(string maGiamGia)
        {
            if (string.IsNullOrWhiteSpace(maGiamGia))
                return RedirectToAction(nameof(Index));

            var today = DateTime.UtcNow.Date;
            var coupon = _context.Coupons.FirstOrDefault(c =>
                c.Ma == maGiamGia && (!c.NgayHetHan.HasValue || c.NgayHetHan.Value.Date >= today));

            if (coupon != null && coupon.GiamGiaPhanTram > 0)
            {
                HttpContext.Session.SetString("MaGiamGia", coupon.Ma);
                HttpContext.Session.SetInt32("GiamGiaPhanTram", coupon.GiamGiaPhanTram.Value);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HuyMaGiamGia()
        {
            HttpContext.Session.Remove("MaGiamGia");
            HttpContext.Session.Remove("GiamGiaPhanTram");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(string? tenNguoiNhan, string? sdtNguoiNhan, string? diaChiGiao)
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (uid == null) return RedirectToAction("DangNhap", "Account");

            var cart = LayGioHang();
            if (!cart.Any())
            {
                TempData["Error"] = "Giỏ hàng trống!";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(tenNguoiNhan) ||
                string.IsNullOrWhiteSpace(sdtNguoiNhan) ||
                string.IsNullOrWhiteSpace(diaChiGiao))
            {
                TempData["Error"] = "Vui lòng nhập đủ tên, SĐT và địa chỉ giao.";
                return RedirectToAction(nameof(Index));
            }

            decimal hangHoa = cart.Sum(c => c.SoLuong * c.Pizza.Gia);
            int giamPct = HttpContext.Session.GetInt32("GiamGiaPhanTram") ?? 0;
            decimal giam = hangHoa * giamPct / 100m;
            decimal tongThanhToan = Math.Max(0, hangHoa - giam);

            var ma = HttpContext.Session.GetString("MaGiamGia");
            var couponId = _context.Coupons.FirstOrDefault(c => c.Ma == ma)?.CouponId;

            var dh = new DonHang
            {
                UserId = uid.Value,
                NgayDat = DateTime.UtcNow,
                TongTien = tongThanhToan,
                TrangThai = "Chờ xử lý",
                CouponId = couponId,
                TenNguoiNhan = tenNguoiNhan!.Trim(),
                SDTNguoiNhan = sdtNguoiNhan!.Trim(),
                DiaChiGiao = diaChiGiao!.Trim()
            };

            _context.DonHangs.Add(dh);
            _context.SaveChanges();

            foreach (var it in cart)
            {
                _context.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    DonHangId = dh.DonHangId,
                    PizzaId = it.Pizza.PizzaId,
                    SoLuong = it.SoLuong,
                    Gia = it.Pizza.Gia,
                    GhiChu = it.GhiChu
                });
            }
            _context.SaveChanges();

            HttpContext.Session.Remove("GioHang");
            HttpContext.Session.Remove("MaGiamGia");
            HttpContext.Session.Remove("GiamGiaPhanTram");

            TempData["Success"] = "Đặt hàng thành công!";
            return RedirectToAction("LichSuDonHang", "DonHang");
        }

        public IActionResult XoaGio()
        {
            HttpContext.Session.Remove("GioHang");
            return RedirectToAction(nameof(Index));
        }

        // ======= Nút "Thêm" nhanh trên danh sách/Menu =======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanh(int pizzaId, string size = "S", int soLuong = 1, string? returnUrl = null)
        {
            if (soLuong < 1) soLuong = 1;

            var pizza = _context.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);
            if (pizza == null) return NotFound();

            var cart = LayGioHang();
            var item = cart.FirstOrDefault(x => x.Pizza.PizzaId == pizzaId);

            if (item == null)
            {
                cart.Add(new CartItem { Pizza = pizza, SoLuong = soLuong });
            }
            else
            {
                item.SoLuong += soLuong;
            }

            LuuGioHang(cart);
            TempData["Success"] = "✔ Đã thêm vào giỏ hàng";

            var back = returnUrl ?? Request.Headers["Referer"].ToString();
            return !string.IsNullOrWhiteSpace(back)
                ? Redirect(back)
                : RedirectToAction(nameof(Index));
        }

        // Lưu ghi chú (post thường - bấm nút Lưu)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatGhiChu(int id, string? note) // id = PizzaId
        {
            var cart = LayGioHang();
            var item = cart.FirstOrDefault(x => x.Pizza.PizzaId == id);
            if (item != null)
            {
                item.GhiChu = (note ?? "").Trim();
                LuuGioHang(cart);
                TempData["Success"] = "Đã lưu ghi chú cho món.";
            }
            return RedirectToAction(nameof(Index));
        }

        // (Tuỳ chọn) Lưu ghi chú bằng AJAX (autosave khi gõ)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatGhiChuAjax(int id, string? note)
        {
            var cart = LayGioHang();
            var item = cart.FirstOrDefault(x => x.Pizza.PizzaId == id);
            if (item == null) return NotFound();

            item.GhiChu = (note ?? "").Trim();
            LuuGioHang(cart);
            return Json(new { ok = true });
        }

    }
}

