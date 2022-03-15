using Paup2022_Vjezba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                RedirectToAction("Popis");

            StudentiDB studentDB = new StudentiDB();

            Student student = studentDB.VratiListu()
                .FirstOrDefault(x => x.ID == id);

            if (student == null)
            {
                RedirectToAction("Popis");
            }
            return View(student);
        }
    }
}