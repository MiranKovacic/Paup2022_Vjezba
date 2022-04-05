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
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.Title = "Početna o studentima";
            ViewBag.Fakultet = "Međimursko veleučilište";
            return View();
        }

        public ActionResult Popis()
        {
            StudentiDB studentDB = new StudentiDB();
            return View(studentDB);
        }

        public ActionResult Detalji(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            StudentiDB studentDB = new StudentiDB();

            Student student = studentDB.VratiListu()
                .FirstOrDefault(x => x.ID == id);

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

            StudentiDB studentDB = new StudentiDB();

            Student student = studentDB.VratiListu()
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
                //Ažuriranje liste podataka
                StudentiDB studentidb = new StudentiDB();
                studentidb.AzurirajStudenta(s);
                return RedirectToAction("Popis");
            }
            return View(s);
        }
    }
}