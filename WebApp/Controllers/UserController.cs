using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Abstract;
using DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(include: "FirstName, LastName, UserName, Password, EmailId")]User user)
        {
            ModelState.Remove("ConfirmPassword");
           
            string msg = "";
            bool status = false;
            if (ModelState.IsValid)
            {
                bool isEmailExist = _userService.IsUserExist(user.EmailId);
                bool isUserNameExist = _userService.IsUserExist(user.UserName);

                if (isEmailExist || isUserNameExist)
                {
                    ModelState.AddModelError("IsExist", "Taki adres email lub nazwa użytkownika już istnieje!");
                    return View(user);
                }

                user.ActivationCode = Guid.NewGuid();
                user.Password = _userService.Hash(user.Password);
                user.EmailConfirmed = false;

                ActivationLink(user.EmailId, user.ActivationCode.ToString());
                _userService.Add(user);
                msg = "Rejestracja zakończona powodzeniem! Link aktywacyjny został wysłany na adres email podany przy rejestracji";
                status = true;

            }
            else
            {
                msg = "Błąd";
            }
            ViewBag.Message = msg;
            ViewBag.Status = status;
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login, string returnUrl)
        {
            string msg = "";
            ViewBag.ReturnUrl = returnUrl;
            var user = _userService.GetAll()
                .Where(x => x.UserName == login.UserName).FirstOrDefault();

            if (user != null)
            {
                if (string.Compare(_userService.Hash(login.Password), user.Password) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 20;

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id, CookieAuthenticationDefaults.AuthenticationScheme),
                        new Claim(ClaimTypes.GivenName, user.UserName, CookieAuthenticationDefaults.AuthenticationScheme),
                        new Claim(ClaimTypes.Role, user.role.ToString(), CookieAuthenticationDefaults.AuthenticationScheme),
                    };

                    var userIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name,
                        ClaimTypes.Role);

                    var principal = new ClaimsPrincipal(userIdentity);

                    var authenticationProperties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                    };

                    var IsVerify = HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        authenticationProperties);
                    await IsVerify;

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), "Home");
                    }
                }
                else
                {
                    msg = "Błędne dane logowania!";
                }

                //if (string.IsNullOrWhiteSpace(returnUrl))
                //{
                //    return RedirectToAction("Index", controllerName: "Home");
                //}
                //else
                //{
                    
                //}

            }

            ViewBag.Message = msg;
            return View();
        }

        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool status = false;

            var user = _userService.GetAll()
                .Where(x => x.ActivationCode == new Guid(id)).FirstOrDefault();

            if (user != null)
            {
                user.EmailConfirmed = true;
                _userService.Update(user);
                status = true;
            }
            else
            {
                ViewBag.Message = "\nTwoje konto nie zostało aktywowane";
                ViewBag.Status = false;
                return View();
            }

            ViewBag.Status = true;
            return View();
        }

        [NonAction]
        public ActionResult ActivationLink(string email, string code)
        {
            var callbackUrl = Url.Action("VerifyAccount", "User",
                new { id = code },
                protocol: Request.Scheme);
            var to = new MailAddress(email);
            string subject = "Twoje konto zostało utworzone!";
            string body = "<br/> Potwierdź swoje konto klikając w link <a href='"+callbackUrl +"'>"+callbackUrl+"</a>";
            MailAddress from = new MailAddress("athpracadyplomowa@gmail.com");

            var message = new MailMessage(from, to)
            {
                From = from,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("athpracadyplomowa@gmail.com", "cokxah-Mijqu9-tifgek"),
                EnableSsl = true,

            };
            smtp.Send(message);

            return View();
        }

    }
}