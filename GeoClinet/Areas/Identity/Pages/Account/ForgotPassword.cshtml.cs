// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mime;

namespace GeoClinet.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await SendEmailAsync(Input.Email, "Reset Password", callbackUrl);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
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
                    <h1>Reset Password</h1>
                </div>
                <div class='content'>
                    <h2>Password Reset Request</h2>
                    <p>We have received a request to reset your password. To proceed with resetting your password, please click on the following link:</p>
                    <a href='{encodedLink}' style='display: inline-block; padding: 10px 20px; font-size: 16px; background-color: #007bff; color: white; text-decoration: none; border: 1px solid #007bff; border-radius: 5px;'>Reset Password</a>
                    <p>If you did not request this action, you can safely ignore this message. Your account security is important to us.</p>
                </div>
                <div class='footer'>
                    <p>Thank you,<br/>The GeoTycoon Team</p>
                </div>
            </div>
        </body>
        </html>";
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                // message.From = new MailAddress("AIAIYan@yandex.com");
                // message.From = new MailAddress("Votuongpro@yandex.com");
                message.From = new MailAddress("khanhtuongadminsp24@geotycoonclient.se");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = confirmLink;

                // Tạo LinkedResource từ hình ảnh và đặt ContentID là ID duy nhất
                LinkedResource linkedImage = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);
                linkedImage.ContentId = cid;
                // Tạo AlternateView với nội dung HTML và thêm LinkedResource
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(newEmailContent, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(linkedImage);
                message.AlternateViews.Add(alternateView);

                smtpClient.Port = 587;
                //smtpClient.Host = "smtp.yandex.com";
                smtpClient.Host = "smtp.simply.com";

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential("Votuongpro", "treuaefycjlhuceg");
                // smtpClient.Credentials = new NetworkCredential("AIAIYan","btmfzuuiinntzcou");
                smtpClient.Credentials = new NetworkCredential("khanhtuongadminsp24@geotycoonclient.se", "Kojlakothe29");
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
    }
}
