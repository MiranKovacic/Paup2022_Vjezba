using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup2022_Vjezba.Models
{
    [Table("studenti")]
    public class Student
    {
        [Key]
        [Display(Name = "ID Studenta")] //Sadržaj html helpera
        public int ID { get; set; }
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        public string PrezimeIme
        {
            get
            {
                return Prezime + " " + Ime;
            }
        }

        [Display(Name = "Spol")]
        public string Spol { get; set; }
        [Display(Name = "OIB")]
        public string OIB { get; set; }
        [Column("datum_rodjenja")]
        [Display(Name = "Datum ređenja")]
        [DisplayFormat (DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }
        [Column("godina_studija")]
        [Display(Name = "Godina studija")]
        public GodinaStudiranja GodinaStudija { get; set; }
        [Column("redovni_student")]
        [Display(Name = "Redovni student")]
        public bool RedovniStudent { get; set; }
        [Column("broj_upisanih_kolegija")]
        [Display(Name = "Broj upisanih kolegija")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [Range(1, 8, ErrorMessage = "Vrijednost {0} mora biti između {1} i {2}")]
        public int BrojUpisanihKolegija { set; get; }
    }
}