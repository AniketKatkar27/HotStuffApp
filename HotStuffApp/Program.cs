using HotStuffApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//
// 1️⃣ Add Database Connection
//
builder.Services.AddDbContext<HotStuffAppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//
// 2️⃣ Add MVC
//
builder.Services.AddControllersWithViews();

//
// 3️⃣ Add Cookie Authentication
//
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

//
// 4️⃣ Add Authorization
//
builder.Services.AddAuthorization();

var app = builder.Build();

//
// 5️⃣ Middleware Pipeline
//

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//
// IMPORTANT ORDER
//
app.UseAuthentication();
app.UseAuthorization();

//
// 6️⃣ Default Route
//
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();