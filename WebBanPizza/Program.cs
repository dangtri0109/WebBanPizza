using System;
using System.Linq;                          // <— cần cho Any()
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using WebBanPizza.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ DbContext dùng PostgreSQL (Render lấy từ biến môi trường ConnectionStrings__DefaultConnection)
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ===== Auto-migrate & seed Admin (chạy khi app start) =====
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

        // Tự apply migrations vào DB Postgres (nếu DB trống sẽ tạo bảng)
        db.Database.Migrate();

        // Tạo Admin mặc định nếu chưa có
        var adminEmail = "admin@pizza.local";
        var adminPass = "123"; // 🔐 đăng nhập xong nhớ đổi ngay
        if (!db.NguoiDungs.Any(u => u.Email == adminEmail))
        {
            db.NguoiDungs.Add(new NguoiDung
            {
                HoTen = "Admin",
                Email = adminEmail,
                MatKhau = adminPass, // ⚠️ nếu bạn có cơ chế hash, báo mình để đổi sang hash
                VaiTro = "Admin"
            });
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Log thô ra console để xem trên Render Logs (tránh crash app)
        Console.WriteLine("Auto-migrate/seed error: " + ex);
    }
}
// ===== End seed =====

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
