using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Domain.Enums;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.Collections;
using NLayerEmarket.Insfastructure.Tools;
using System.Security.Claims;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.ViewModels;
using NLayerEmarket.Application.Collections.Basket;

namespace NLayerEmarket.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserReadRepository _userReadRepository;
        private readonly IBasketCollection _basketCollection;



        public AuthController(IUserReadRepository userReadRepository, IBasketCollection basketCollection)
        {
            _userReadRepository = userReadRepository;
            _basketCollection = basketCollection;
        }


        public IActionResult Login()
        {

            return View();
        }

        [Route("/auth/LoginControl/")]
        [HttpPost]
        public bool LoginControl(LoginVM model)
        {


            User? loginControl = _userReadRepository.GetWhere(x => x.Mail == model.Mail && x.Password == HashPass.hashPass(model.Password)).FirstOrDefault();



            ClaimsIdentity? identity = null;

            if (loginControl != null)
            {
                HttpContext.Session.SetString("ID", loginControl.ID.ToString());
                HttpContext.Session.SetString("Role", (loginControl.Role.ToString()));
                HttpContext.Session.SetString("Name", loginControl.Name + " " + loginControl.Surname);
                HttpContext.Session.SetString("Mail", loginControl.Mail);

                BasketModel? basket = _basketCollection.Get().LastOrDefault(x => x.userId == HttpContext.Session.GetString("ID"));

                if (basket != null)
                {
                    HttpContext.Session.SetObject("Basket", basket.ProductList);

                }

                var usurRole = loginControl.Role;
                switch (usurRole)
                {
                    case Role.Admin:
                        identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), Role.Admin)));
                        break;
                    case Role.User:
                        identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), Role.User)));
                        break;
                };

                ClaimsPrincipal? principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(30),
                    IsPersistent = false,
                    AllowRefresh = false
                });
                return true;
            }

            else
            {
                return false;
            }




        }

        [Route("/auth/Logout/")]
        public bool Logout()
        {
            HttpContext.Session.Remove("KisiID");
            HttpContext.Session.Remove("Ad");
            HttpContext.Session.Remove("Email");
            if (HttpContext.Session.GetObject<List<ProductVM>>("Basket") != null)
            {
                HttpContext.Session.Remove("Basket");

            }
            return true;
        }
    }
}
