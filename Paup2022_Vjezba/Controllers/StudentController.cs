using PagedList;
using Paup2022_Vjezba.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Paup2022_Vjezba.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        BazaDbContext bazaPOdataka = new BazaDbContext();
        // GET: Student

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Početna o studentima";
            ViewBag.Fakultet = "Međimursko veleučilište";
            return View();
        }


        [AllowAnonymous]
        public ActionResult Popis(string naziv, string spol, string smjer)
        {
            var smjeroviList = bazaPOdataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            ViewBag.Smjerovi = smjeroviList;
            return View();
        }


        [AllowAnonymous]
        public ActionResult PopisPartial(string naziv, string spol, string sort, string smjer, int? page)
        {
            ViewBag.Sortiranje = sort;
            ViewBag.NavizSort = String.IsNullOrEmpty(sort) ? "naziv_desc" : "";
            ViewBag.SmjerSort = sort == "smjer" ? "smjer_desc" : "smjer";
            ViewBag.Smjer = smjer;
            ViewBag.Naziv = naziv;
            ViewBag.Spol = spol;

            var studenti = bazaPOdataka.PopisStudenata.ToList();
            var smjeroviList = bazaPOdataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            //ViewBag.Smjerovi = smjeroviList;
            if (!String.IsNullOrWhiteSpace(naziv))
                studenti = studenti.Where(x => x.PrezimeIme.ToUpper().Contains(naziv.ToUpper())).ToList();
            if (!String.IsNullOrWhiteSpace(spol))
                studenti = studenti.Where(x => x.Spol == spol).ToList();
            if (!String.IsNullOrWhiteSpace(smjer))
                studenti = studenti.Where(x => x.SifraSmjera == smjer).ToList();

            switch (sort)
            {
                case "naziv_desc":
                    studenti = studenti.OrderByDescending(s => s.PrezimeIme).ToList();
                    break;
                case "smjer":
                    studenti = studenti.OrderBy(s => s.SifraSmjera).ToList();
                    break;
                case "smejr_desc":
                    studenti = studenti.OrderByDescending(s => s.SifraSmjera).ToList();
                    break;
                default:
                    studenti = studenti.OrderBy(s => s.PrezimeIme).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return PartialView("_PartialPopis", studenti.ToPagedList(pageNumber, pageSize));
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
            Student student = null;
            if (!id.HasValue)
            {
                student = new Student();
                ViewBag.Title = "Kreiranje studenta";
                ViewBag.Novi = true;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                student = bazaPOdataka.PopisStudenata
                    .FirstOrDefault(s => s.ID == id);

                if (student == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Title = "Ažuriranje postojećeg studenta";
                ViewBag.Novi = false;
            }

            var smjerovi = bazaPOdataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            smjerovi.Insert(0, new Smjer { Sifra = "", Naziv = "Nedefinirano" });
            ViewBag.Smjerovi = smjerovi;

            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Student s)
        {
            if(s.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                string extension = Path.GetExtension(s.ImageFile.FileName);
                if(extension == ".jepg" || extension == ".jpg" || extension == ".png")
                {
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    s.SlikaPutanja = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    s.ImageFile.SaveAs(fileName);
                }
                else
                {
                    ModelState.AddModelError("SlikaPutanja", "Nepodržana extenzija");
                }
            }
            if (ModelState.IsValid)
            {
                if (s.ID != 0)
                    bazaPOdataka.Entry(s).State = System.Data.Entity.EntityState.Modified;
                else
                    bazaPOdataka.PopisStudenata.Add(s);
                bazaPOdataka.SaveChanges();
                return RedirectToAction("Popis");
            }

            if(s.ID!=0)
            {
                ViewBag.Title = "Ažuriranje studenta";
                ViewBag.Novi = false;
            }
            else
            {
                ViewBag.Title = "Kreiranje novog studenta";
                ViewBag.Novi = true;
            }
            return View(s);
        }

        public ActionResult Brisi(int? id)
        {
            if (id == null)
                return RedirectToAction("Popis");
            Student s = bazaPOdataka.PopisStudenata.FirstOrDefault(x => x.ID == id);
            if (s == null)
                return HttpNotFound();
            ViewBag.Title = "Potvrda brisanja studenta";
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            Student s = bazaPOdataka.PopisStudenata
                .FirstOrDefault(x => x.ID == id);
            if (s == null)
                return HttpNotFound();
            bazaPOdataka.PopisStudenata.Remove(s);
            bazaPOdataka.SaveChanges();
            return View("BrisiStatus");
        }
    }
}