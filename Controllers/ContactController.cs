using ContactAppEF.Data;
using ContactAppEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactAppEF.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        // GET: Contact
        private readonly MyContext _myContext = new MyContext();
        [Authorize(Roles ="User,Admin")]
        public ActionResult Index()
        {
            var contacts=_myContext.Contacts.Include(c=>c.User).ToList();
            return View(contacts);
        }
        [HttpGet]
        public ActionResult CreateContact()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            _myContext.Contacts.Add(contact);
            _myContext.SaveChanges();
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _myContext.Contacts.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }

    

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            var data = _myContext.Contacts.Where(x => x.Id == contact.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = contact.Name;
                _myContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
   
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = _myContext.Contacts.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
      
        [HttpPost]
        public ActionResult Delete(Contact contact)
        {
            var data = _myContext.Contacts.Where(x => x.Id == contact.Id).FirstOrDefault();
            if (data != null)
            {
                _myContext.Contacts.Remove(data);
                _myContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult GetContactDetails(int id)
        {
            var data = _myContext.Contacts.Where(x => x.Id == id).Include(x => x.Details).FirstOrDefault();
            return View(data);
        }
    }
}