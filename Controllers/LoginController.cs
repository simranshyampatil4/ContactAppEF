using ContactAppEF.Data;
using ContactAppEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ContactAppEF.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly MyContext _myContext = new MyContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            var data = _myContext.Users.Where(u => u.Name == user.Name && u.Password == user.Password);
            if (data.Count() != 0)
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "Invalid username or password.";
            }
            return View();
            
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}