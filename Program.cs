using MyWebSite.Services.IServices;
using MyWebSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC servislerini ekle
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, EnvUserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin-login-3477";  // doğru rota
        options.AccessDeniedPath = "/";
    });
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 2. Hata ay�klama i�in geli�tirme ortam� kontrol�
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 3. HTTPS ve statik dosya ayarlar�
app.UseHttpsRedirection();
app.UseStaticFiles();

// 4. Routing ve (ileride) Authentication middleware'leri
app.UseRouting();
app.UseAuthentication(); // ileride giri� sistemi ekledi�imizde �al��acak
app.UseAuthorization();

// 5. Default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MainPage}/{action=MainPage}/{id?}");

app.Run();
