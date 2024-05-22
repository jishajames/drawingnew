using drawingnew.Models;
using drawingnew.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;



namespace drawingnew.Pages
{
    public class UserLoginModel : PageModel
    {
        private readonly IUserAuthenticationService _userAuthentication;
        public UserLoginModel(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }
        public void OnGet()
        {
            LoginUser = new Login();
        }
        [BindProperty]
        public required Login LoginUser { get; set; }
        public IActionResult OnPost(string url)
        {
            if(string.IsNullOrEmpty(url))
                url = Url.Page("Drawmethods/DrawShapes");


            var isValidLogin = _userAuthentication.GetLoginUserAuthentication(LoginUser,  out Login user);

            if (!isValidLogin)
                ModelState.AddModelError("Username", "Incorrect User Name or Password");
            else if (!user.IsAdmin)
                ModelState.AddModelError("Username", "Not authorized to access this page");
            else
            {
                SignInUser(user);
                return Redirect(url);
            }
            return Page();



        }
        private void SignInUser(Login user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Administrator" : "User"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                RedirectUri = Url.Page("/Account/Login")
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
