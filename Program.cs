
using OpticalFibersRetailShop.DAL;
using OpticalFibersRetailShop.Models.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")), ServiceLifetime.Transient);

builder.Services.AddRazorPages();
builder.Services.AddCors();

double sessionTimeout = Convert.ToDouble(builder.Configuration["sessionTimeOut"]);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Registering Repositories and Services
builder.Services.AddScoped<IUserRepo, UserRepo>();
// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authenticate/Login";
        options.AccessDeniedPath = "/Authenticate/Login";
    });

// Authorization Policies
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Super Admin", policy =>
//        policy.RequireClaim("UserType", "Super Admin", "School Admin", "Teacher"));
//    options.AddPolicy("School Admin", policy =>
//        policy.RequireClaim("UserType", "School Admin", "Student", "Teacher"));
//    options.AddPolicy("Teacher", policy =>
//        policy.RequireClaim("UserType", "Teacher"));
//    //options.AddPolicy("HRAccess", policy =>
//    //    policy.RequireClaim("UserType", "HR"));
//    //options.AddPolicy("EmployeeOrHRAccess", policy =>
//    //   policy.RequireRole("EmployeeAccess", "HR"));
//});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Login}/{id?}");

app.Run();
