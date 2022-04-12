using Paup2022_Vjezba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Paup2022_Vjezba.Controllers
{
    public class StudentController : Controller
    {
        BazaDbContext bazaPOdataka = new BazaDbContext();
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.Title = "Početna o studentima";
            ViewBag.Fakultet = "Međimursko veleučilište";
            return View();
        }

        public ActionResult Popis()
        {
            var studenti = bazaPOdataka.PopisStudenata.ToList();
            return View(studenti);
        }

        public ActionResult Detalji(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Student student = bazaPOdataka.PopisStudenata.FirstOrDefault(x => x.ID == id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Azuriraj(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Student student = bazaPOdataka.PopisStudenata
                .FirstOrDefault(s => s.ID == id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Student s)
        {
            if (ModelState.IsValid)
            {
                bazaPOdataka.Entry(s).State = System.Data.Entity.EntityState.Modified;
                bazaPOdataka.SaveChanges();
                return RedirectToAction("Popis");
            }
            return View(s);
        }
    }
}