using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TP5WebTransactionnel.DataAccessLayer;
using TP5WebTransactionnel.Helpers;
using TP5WebTransactionnel.Models;
using TP5WebTransactionnel.Resources;
using TP5WebTransactionnel.ViewModels;

namespace TP5WebTransactionnel.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Login()
        {
            Member member = new DAL().MemberFact.CreateEmpty();
            MemberLoginViewModel viewModel = new MemberLoginViewModel(member);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(MemberLoginViewModel viewModel, string returnurl)
        {
            if (ModelState.IsValid)
            {
                DAL dal = new DAL();
                Member user = dal.MemberFact.GetByUsername(viewModel.Username);

                if (user != null)
                {
                    //bool valid = viewModel.Password == user.Password;
                    bool valid = CryptographyHelper.ValidateHashedPassword(viewModel.Password, user.Password);

                    if (valid)
                    {
                        //Create the identity for the user  
                        var identity = new ClaimsIdentity(new[] {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.Role, user.Role)
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                        

                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                        if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
                        {
                            if (user.Role == Member.ROLE_STANDARD && returnurl.ToLower().StartsWith("/admin"))
                            {
                                return RedirectToAction("Index", "Home", new { Area = "" });
                            }

                            return LocalRedirect(returnurl);
                        }
                        else if (user.Role == Member.ROLE_ADMIN)
                        {
                            return RedirectToAction("List", "Reservation", new { Area = "Admin" });
                        }
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                }

                ModelState.AddModelError("Password", Resource.InvalidUsernamePassword);
            }

            return View(viewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member member)
        {
            return RedirectToAction("Login");
        }
    }
}
