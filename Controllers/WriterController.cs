using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

       
        [Authorize]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            Context c = new Context();
            var writerName = c.Writers.Where(x => x.WriteMail == usermail).Select(y => y.WriteMail).FirstOrDefault();
            ViewBag.v2 = writerName;
            return View();
        }
        public IActionResult WriterProfile()
        {
            return View();
        }
        public IActionResult WriterMail()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }
        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
    
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            Context c = new Context();
            var usermail = User.Identity.Name;

            var writerID = c.Writers.Where(x => x.WriteMail == usermail).
                Select(y => y.WriterID).FirstOrDefault();
            var writervalues= wm.TGetById(writerID);
            return View(writervalues);
        }
       
        [HttpPost]
        public IActionResult WriterEditProfile(Writer p )
        {
            WriterValidator wl = new WriterValidator();
            ValidationResult results = wl.Validate(p);
            if (results.IsValid)
            {
                wm.TUpdate(p);
                return RedirectToAction("Index", "Dashboard");
                
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName,
                        item.ErrorMessage);
                }
            }
            return View();


        }
        [AllowAnonymous]
        [HttpGet]

        public IActionResult WriterAdd()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]

        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.WriteImage != null)
            {
                var extension = Path.GetExtension(p.WriteImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriteImage.CopyTo(stream);
                w.WriteImage = newimagename;
            }
            w.WriteMail = p.WriteMail;
            w.WriterName = p.WriterName;
            w.WritePassword = p.WritePassword;
            w.WriterStatus = true;
            w.WriterAbout = p.WriterAbout;
             
            wm.TAdd(w);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
