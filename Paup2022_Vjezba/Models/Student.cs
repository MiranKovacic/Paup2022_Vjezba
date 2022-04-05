using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paup2022_Vjezba.Models
{
    public class Student
    {
        [Display(Name = "ID Studenta")] //Sadržaj html helpera
        public int ID { get; set; }
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [Display(Name = "Spol")]
        public char Spol { get; set; }
        [Display(Name = "OIB")]
        public string OIB { get; set; }
        [Display(Name = "Datum ređenja")]
        [DisplayFormat (DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }
        [Display(Name = "Godina studija")]
        public GodinaStudiranja GodinaStudija { get; set; }
        [Display(Name = "Redovni student")]
        public bool RedovniStudent { get; set; }
    }
}