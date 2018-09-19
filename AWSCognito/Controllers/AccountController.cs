using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace AWSCognito.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
                var callbackUrl = Url.Action(nameof(SignedOut), "Account", values: null, protocol: Request.Scheme);

                return SignOut(

                    new AuthenticationProperties { RedirectUri = callbackUrl },

                    CookieAuthenticationDefaults.AuthenticationScheme,

                    OpenIdConnectDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }
    }
}