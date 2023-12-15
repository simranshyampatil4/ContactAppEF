using ContactAppEF.Data;
using ContactAppEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ContactAppEF.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly MyContext _myContext = new MyContext();
        public ActionResult Index()
        {
            var users =_myContext.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            _myContext.Users.Add(user);
            _myContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _myContext.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var data = _myContext.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = user.Name;
                _myContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model)
        {
            bool IsVaildUser = _myContext.Users.Any(user=>user.Name.ToLower()==model.Name.ToLower()
            && user.Password==model.Password);
            if(IsVaildUser)
            {
                FormsAuthentication.SetAuthCookie(model.Name, false);
                var role = _myContext.Users.Where(u => u.Name == model.Name).Include(u => u.Role).FirstOrDefault().Role.Name;
                if (role == "Admin")
                    return RedirectToAction("Index","Home");
                return RedirectToAction("Index", "Contact");
            }
            ModelState.AddModelError("", "invalid username or password");
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult GetContacts(int id)
        {
            var data = _myContext.Users.Where(x => x.Id == id).Include(x=> x.Contacts).FirstOrDefault();
            return View(data);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = _myContext.Users.Where(x=>x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(User user)
        {
            var data = _myContext.Users.Where(x=>x.Id==user.Id).FirstOrDefault();
            if(data != null)
            {
                _myContext.Users.Remove(data);
                _myContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
    }
}