using System;
using System.Collections.Generic;
using System.Linq;

namespace Paup2022_Vjezba.Models
{
    public class StudentiDB
    {
        private static List<Student> lista = new List<Student>();
        private static bool listaInicijalizirana = false;

        public StudentiDB()
        {

            if (listaInicijalizirana == false)
            {

                listaInicijalizirana = true;
                lista.Add(new Student()
                {
                    ID = 1,
                    Prezime = "Ivić",
                    Ime = "Petar",
                    Spol = "M",
                    DatumRodjenja = new DateTime(1991, 02, 01),
                    GodinaStudija = GodinaStudiranja.Druga,
                    OIB = "12345678901",
                    RedovniStudent = true
                });
                lista.Add(new Student()
                {
                    ID = 2,
                    Prezime = "Mat",
                    Ime = "Milivoj",
                    Spol = "M",
                    DatumRodjenja = new DateTime(1958, 2, 15),
                    GodinaStudija = GodinaStudiranja.Druga,
                    OIB = "10987654321",
                    RedovniStudent = false
                });
                lista.Add(new Student()
                {
                    ID = 3,
                    Prezime = "Bes",
                    Ime = "Marta",
                    Spol = "Ž",
                    DatumRodjenja = new DateTime(1987, 11, 12),
                    GodinaStudija = GodinaStudiranja.Prva,
                    OIB = "13579086422",
                    RedovniStudent = true
                });
                lista.Add(new Student()
                {
                    ID = 4,
                    Prezime = "Perko",
                    Ime = "Mia",
                    Spol = "Ž",
                    DatumRodjenja = new DateTime(1990, 04, 26),
                    GodinaStudija = GodinaStudiranja.Peta,
                    OIB = "10293847564",
                    RedovniStudent = true
                });
            }
        }

        public List<Student> VratiListu()
        {
            return lista;
        }

        public void AzurirajStudenta(Student student)
        {
            int studentIndex = lista.FindIndex(s => s.ID == student.ID);
            lista[studentIndex] = student;
        }

    }
}