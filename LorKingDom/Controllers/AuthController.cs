using System.Globalization;
using LorKingDom.Models;
using LorKingDom.Services;
using LorKingDom.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LorKingDom.Controllers
{
    public class AccountController : Controller
    {
        private readonly LorKingDomContext _db;
        private readonly IPasswordHasher<Account> _hasher;
        private readonly IEmailSender _email;

        public AccountController(
            LorKingDomContext db,
            IPasswordHasher<Account> hasher,
            IEmailSender email)
        {
            _db     = db;
            _hasher = hasher;
            _email  = email;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
            => View(new RegisterViewModel());

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // 1. Kiểm tra trùng Email / Phone
            if (await _db.Accounts.AnyAsync(a => a.Email == vm.Email))
            {
                ModelState.AddModelError(nameof(vm.Email), "Email đã được sử dụng");
                return View(vm);
            }
            if (await _db.Accounts.AnyAsync(a => a.PhoneNumber == vm.PhoneNumber))
            {
                ModelState.AddModelError(nameof(vm.PhoneNumber), "Số điện thoại đã được sử dụng");
                return View(vm);
            }

            // 2. Tạo Account và lưu ngay với EmailConfirmed = false
            var user = new Account
            {
                AccountName    = vm.UserName,
                Email          = vm.Email,
                PhoneNumber    = vm.PhoneNumber,
                Password       = _hasher.HashPassword(null, vm.Password),
                CreatedAt      = DateTime.UtcNow,
                Status         = "Inactive",
                EmailConfirmed = false,
                IsDeleted      = false,
            };
            _db.Accounts.Add(user);
            await _db.SaveChangesAsync();

            // 3. Sinh mã & expiry, lưu tạm (TempData) và gửi mail
            var code   = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(2);

            await (_email as EmailSender)!.SendVerificationEmailAsync(
                to:       vm.Email,
                subject:  "Xác thực tài khoản LorKingDom",
                userName: vm.UserName,
                code:     code
            );

            var regInfo = new
            {
                vm.UserName,
                vm.Email,
                vm.PhoneNumber,
                vm.Password,
                Code   = code,
                Expiry = expiry.ToString("o")
            };
            TempData["RegInfo"]     = JsonConvert.SerializeObject(regInfo);
            TempData["VerifyEmail"] = vm.Email;

            // 4. Redirect sang trang Verify
            return RedirectToAction(nameof(Verify));
        }

        // GET: /Account/Verify
        [HttpGet]
        public IActionResult Verify()
        {
            var email = TempData["VerifyEmail"] as string;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction(nameof(Register));

            TempData.Keep("RegInfo");
            TempData.Keep("VerifyEmail");

            return View(new VerifyViewModel { Email = email });
        }

        // POST: /Account/Verify
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(VerifyViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var json = TempData["RegInfo"] as string;
            if (string.IsNullOrEmpty(json))
            {
                ModelState.AddModelError("", "Phiên làm việc đã hết hạn. Vui lòng đăng ký lại.");
                return View(vm);
            }

            dynamic regInfo     = JsonConvert.DeserializeObject<dynamic>(json)!;
            string expectedCode = (string)regInfo.Code;
            DateTime expiry     = DateTime.Parse((string)regInfo.Expiry);

            if (vm.Code != expectedCode || expiry < DateTime.UtcNow)
            {
                ModelState.AddModelError(nameof(vm.Code), "Mã không hợp lệ hoặc đã hết hạn.");
                TempData["RegInfo"]     = json;
                TempData["VerifyEmail"] = vm.Email;
                return View(vm);
            }

            // 5. Cập nhật Account đã tồn tại
            var user = await _db.Accounts
                .SingleOrDefaultAsync(a => a.Email == vm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy tài khoản.");
                return View(vm);
            }

            user.EmailConfirmed = true;
            user.Status         = "Active";
            await _db.SaveChangesAsync();

            return View("VerifySuccess");
        }

        // POST: /Account/ResendCode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendCode(string email)
        {
            var user = await _db.Accounts
                .SingleOrDefaultAsync(a => a.Email == email);
            if (user == null)
                return NotFound();

            // Chỉ cho resend nếu chưa xác thực
            if (user.EmailConfirmed)
                return BadRequest("Tài khoản đã được xác thực.");

            var code   = new Random().Next(100000, 999999).ToString();
            var expiry = DateTime.UtcNow.AddMinutes(2);

            var regInfo = new
            {
                UserName = user.AccountName,
                user.Email,
                user.PhoneNumber,
                // password không cần gửi lại
                Password = (string?)null,
                Code   = code,
                Expiry = expiry.ToString("o")
            };
            TempData["RegInfo"]     = JsonConvert.SerializeObject(regInfo);
            TempData["VerifyEmail"] = email;

            await (_email as EmailSender)!.SendVerificationEmailAsync(
                to:       email,
                subject:  "Xác thực tài khoản LorKingDom",
                userName: user.AccountName,
                code:     code
            );

            return RedirectToAction(nameof(Verify));
        }
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if(!ModelState.IsValid) return View(vm);
            var user = await _db.Accounts.SingleOrDefaultAsync(a => a.Email == vm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                return View(vm);
            }
            var verify = _hasher.VerifyHashedPassword(user, user.Password, vm.Password);
            if (verify != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
                return View(vm);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Tài khoản chưa được kích hoạt. Vui lòng kiểm tra email để được xác thức");
                return View(vm);
            }
            
            HttpContext.Session.SetInt32("AccountId", user.AccountId);
            HttpContext.Session.SetString("AccountName", user.AccountName);
            TempData["SuccessMessage"] = $"Đăng nhập thành công! Chào mừng {user.AccountName}.";
            return RedirectToAction("Index", "Home");
        }
        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}