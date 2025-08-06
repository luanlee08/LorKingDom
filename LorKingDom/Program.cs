using LorKingDom.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using LorKingDom.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace LorKingDom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailSettings"));

            // 2. Đăng ký EmailSender để inject IEmailSender
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            // 3. Đăng ký PasswordHasher cho Account (đã dùng trong AuthController)
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

            // 1. Kết nối database
            builder.Services.AddDbContext<LorKingDomContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();


            // 4) Session để lưu userId sau login
            builder.Services.AddSession();

            // 3. Add MVC + Authorizationz
            builder.Services.AddAuthorization();
            builder.Services.AddControllersWithViews();

            // ✅ Build app sau khi cấu hình dịch vụ
            var app = builder.Build();

            // 4. Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();  
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}