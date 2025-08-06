using System;
using System.IO;
using System.Threading.Tasks;
using LorKingDom.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace LorKingDom.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _cfg;
        private readonly IWebHostEnvironment _env;

        public EmailSender(IOptions<EmailSettings> cfg, IWebHostEnvironment env)
        {
            _cfg = cfg.Value;
            _env = env;
        }

        public Task SendEmailAsync(string email, string subject, string html) =>
            SendRawEmail(email, subject, html);

        public async Task SendVerificationEmailAsync(string to, string subject, string userName, string code)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_cfg.FromName, _cfg.FromEmail));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;

            var builder = new BodyBuilder();

            // Nhúng banner
            var bannerPath = Path.Combine(_env.WebRootPath, "img-gmail", "banner.jpeg");
            var bannerCid = "";
            if (File.Exists(bannerPath))
            {
                var banner = builder.LinkedResources.Add(bannerPath);
                banner.ContentId = MimeUtils.GenerateMessageId();
                bannerCid = banner.ContentId;
                Console.WriteLine($"Đường dẫn: {bannerPath}");
            }

            var logoPath = Path.Combine(_env.WebRootPath, "img-gmail", "logo.png");
            var logoCid = "";
            if (File.Exists(logoPath))
            {
                var logo = builder.LinkedResources.Add(logoPath);
                logo.ContentId = MimeUtils.GenerateMessageId();
                logoCid = logo.ContentId;
            }

            var imgTag = string.IsNullOrEmpty(bannerCid)
                ? ""
                : $"<img src=\"cid:{bannerCid}\" style=\"width:100%;display:block;margin-bottom:20px;\"/>";

            builder.HtmlBody = $@"
                <!DOCTYPE html>
                <html>
                  <body style='margin:0;padding:0;font-family:Arial,sans-serif;color:#333;'>
                    <div style='max-width:600px;margin:0 auto;padding:20px;'>
                      {imgTag}
                      <p style='font-size:16px;line-height:1.5;margin-bottom:20px;'>
                        Xin chào <strong>{userName}</strong>,
                      </p>
                      <p style='font-size:16px;line-height:1.5;margin-bottom:20px;'>
                        Chúc mừng bạn đã trở thành thành viên của <strong>LorKingDom</strong> – thiên đường đồ chơi dành cho các bé yêu! 🎉
                      </p>
                      <p style='font-size:16px;line-height:1.5;margin-bottom:20px;'>
                        Tại LorKingDom, chúng tôi luôn tâm niệm mang đến cho các bé:
                      </p>
                      <ul style='font-size:16px;line-height:1.5;margin-bottom:20px;'>
                        <li>Đồ chơi an toàn, được sản xuất từ chất liệu phi độc hại</li>
                        <li>Thiết kế sáng tạo, kích thích phát triển tư duy</li>
                        <li>Giá cả hợp lý và dịch vụ chăm sóc khách hàng tận tâm</li>
                      </ul>
                      <p style='font-size:16px;line-height:1.5;margin-bottom:20px;'>
                        Hãy nhập mã xác minh bên dưới để kích hoạt tài khoản và khám phá ngay vô vàn món đồ chơi thú vị cho bé:
                      </p>
                      <h2 style='text-align:center;color:#007bff;margin:20px 0;'>{code}</h2>
                      <p style='font-size:14px;color:#666;text-align:center;margin-bottom:30px;'>
                        Mã này sẽ hết hạn sau <strong>2 phút</strong>. Nếu bạn không phải là chủ nhân của yêu cầu này, vui lòng bỏ qua email.
                      </p>

                      <div style='border-top:1px solid #eee;padding-top:10px;text-align:center;font-size:12px;color:#999;'>
                        Email: <a href='mailto:{_cfg.FromEmail}' style='color:#007bff;text-decoration:none;'>{_cfg.FromEmail}</a> • 09123456789<br/>
                        Website: <a href='https://lorKingdom.com.vn' style='color:#007bff;text-decoration:none;'>lorKingdom.com.vn</a>
                      </div>
                    </div>
                  </body>
                </html>";

            msg.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_cfg.Host, _cfg.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_cfg.Username, _cfg.Password);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }

        private async Task SendRawEmail(string to, string subject, string html)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_cfg.FromName, _cfg.FromEmail));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = html };
            msg.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_cfg.Host, _cfg.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_cfg.Username, _cfg.Password);
            await smtp.SendAsync(msg);
            await smtp.DisconnectAsync(true);
        }
    }
}