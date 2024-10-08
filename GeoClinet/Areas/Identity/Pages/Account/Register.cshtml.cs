﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

namespace GeoClinet.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            public string DOB { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if (!ValidateDOB(Input.DOB))
                {
                    ModelState.AddModelError("Input.DOB", "Date of Birth must not exceed the year 2020.");
                    return Page();
                }
                var user = CreateUser();
                await _userManager.AddToRoleAsync(user, Input.Role);
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await SendEmailAsync(Input.Email, "Confirm your email", callbackUrl);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        private bool ValidateDOB(string dob)
        {
            if (DateTime.TryParse(dob, out DateTime date))
            {
                if (date.Year <= 2020)
                {
                    return true;
                }
            }
            return false;
        }
        public static async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
        {
            try
            {
                var encodedLink = HtmlEncoder.Default.Encode(confirmLink);
                string imagePath = Path.Combine("wwwroot", "css", "GeoTycoonLogo_Trans.png"); // Tạo đường dẫn tuyệt đối đến hình ảnh
                string cid = Guid.NewGuid().ToString(); // Tạo một ID duy nhất cho hình ảnh
                string newEmailContent = $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                    color: #333333;
                    background-color: #f4f4f4;
                    padding: 0;
                    margin: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #ffffff;
                    border: 1px solid #e0e0e0;
                    border-radius: 5px;
                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    text-align: center;
                    margin-bottom: 20px;
                }}
                .header img {{
                    width: 80px;
                    height: auto;
                    display: block;
                    margin: 0 auto;
                }}
                .header h1 {{
                    font-size: 24px;
                    color: #333333;
                    margin: 10px 0;
                }}
                .order-number {{
                    text-align: center;
                    font-size: 14px;
                    color: #777777;
                    margin-bottom: 20px;
                }}
                .content {{
                    text-align: center;
                    margin-bottom: 20px;
                }}
                .content h2 {{
                    font-size: 20px;
                    color: #333333;
                    margin-bottom: 10px;
                }}
                .content p {{
                    font-size: 14px;
                    color: #555555;
                    margin-bottom: 20px;
                }}
                .footer {{
                    text-align: center;
                    font-size: 0.9em;
                    color: #777777;
                }}
                .btn {{
                    display: inline-block;
                    padding: 10px 20px;
                    font-size: 16px;
                    color: #ffffff;
                    background-color: #f0ad4e;
                    border-radius: 5px;
                    text-decoration: none;
                    margin-top: 20px;
                }}
                .btn:hover {{
                    background-color: #ec971f;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <img src='cid:{cid}' alt='Company Logo'> <!-- Sử dụng ID của hình ảnh trong thẻ img -->
                    <h1>Welcome To GeoTycoon!</h1>
                </div>
                <div class='content'>
                    <h2>Thank You For Registering</h2>
                    <p>You are registering with ""Teacher"" rights, your account will need time to be approved, please wait two minutes.</p>
                    <p>We would like to thank you for registering an account. To complete the registration process, please click on the following link to confirm registration:</p>
                    <a href='{encodedLink}' style='display: inline-block; padding: 10px 20px; font-size: 16px; background-color: #007bff; color: white; text-decoration: none; border: 1px solid #007bff; border-radius: 5px;'>Click Here</a>
                    <p>If you do not take this action, your account may not be activated and some features may be limited.</p>
                </div>
                <div class='footer'>
                    <p>Thank you,<br/>The GeoTycoon Team</p>
                </div>
            </div>
        </body>
        </html>";

                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("khanhtuongadminsp24@geotycoonclient.se");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true; // Important to set this to true for HTML content
                message.Body = newEmailContent;

                // Tạo LinkedResource từ hình ảnh và đặt ContentID là ID duy nhất
                LinkedResource linkedImage = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);
                linkedImage.ContentId = cid;
                // Tạo AlternateView với nội dung HTML và thêm LinkedResource
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(newEmailContent, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(linkedImage);
                message.AlternateViews.Add(alternateView);

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.simply.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("khanhtuongadminsp24@geotycoonclient.se", "Kojlakothe3002@");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
