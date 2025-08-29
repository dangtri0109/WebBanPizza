using System;
using System.Linq;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using WebBanPizza.Models;

var builder = WebApplication.CreateBuilder(args);

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DbContext PostgreSQL
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ===== Auto-migrate & seed on startup =====
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

        // Apply migrations
        db.Database.Migrate();

        // Seed Admin mặc định
        var adminEmail = "admin@pizza.local";
        var adminPass = "123"; // ⚠️ demo thôi, cần hash khi dùng thật
        if (!db.NguoiDungs.Any(u => u.Email == adminEmail))
        {
            var admin = new NguoiDung
            {
                HoTen = "Admin",
                Email = adminEmail,
                MatKhau = adminPass,
                VaiTro = "Admin"
            };
            db.NguoiDungs.Add(admin);
            db.SaveChanges();
        }

        // Seed DanhMuc mặc định nếu trống
        if (!db.DanhMucs.Any())
        {
            db.DanhMucs.AddRange(
                new DanhMuc { TenDanhMuc = "Hải sản" },
                new DanhMuc { TenDanhMuc = "Bò" },
                new DanhMuc { TenDanhMuc = "Gà" },
                new DanhMuc { TenDanhMuc = "Rau củ" }
            );
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Auto-migrate/seed error: " + ex.Message);
        if (ex.InnerException != null)
            Console.WriteLine("Inner: " + ex.InnerException.Message);
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
