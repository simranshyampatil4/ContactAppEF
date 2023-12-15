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
    public class DetailController : Controller
    {
        // GET: Detail
        private readonly MyContext _myContext = new MyContext();
        public ActionResult Index()
        {
            var details = _myContext.Details.Include(d=>d.Contact).ToList();
            return View(details);
        }
       
        [HttpGet]
        public ActionResult AddContactDetails()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult AddContactDetails(Detail detail)
        {
            _myContext.Details.Add(detail);
            _myContext.SaveChanges();
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _myContext.Details.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
       
        [HttpPost]
        public ActionResult Edit(Detail detail)
        {
            var data = _myContext.Details.Where(x => x.Id == detail.Id).FirstOrDefault();
            if (data != null)
            {
                data.Email = detail.Email;
                _myContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = _myContext.Details.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
       
        [HttpPost]
        public ActionResult Delete(Detail detail)
        {
            var data = _myContext.Details.Where(x => x.Id == detail.Id).FirstOrDefault();
            if (data != null)
            {
                _myContext.Details.Remove(data);
                _myContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}