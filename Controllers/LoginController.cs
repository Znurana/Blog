using DataAccessLayer.Concreate;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public  async Task <IActionResult> Index(Writer p)
        {
            Context c = new Context();
            var datavalue = c.Writers.FirstOrDefault(x => x.WriteMail == p.WriteMail && x.WritePassword == p.WritePassword);
            if(datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, p.WriteMail)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
          

        }

    }
}
//Context c = new Context();
//var datavalue = c.Writers.FirstOrDefault(x => x.WriteMail == p.WriteMail && x.WritePassword == p.WritePassword);
//if (datavalue != null)
//{
//    HttpContext.Session.SetString("username", p.WriteMail);
//    return RedirectToAction("Index", "Writer");
//}
//else
//{
//    return View();
//}
