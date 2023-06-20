using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string Klasa { get; set; }

        // Można dodać listę ocen studenta, jeśli jest to potrzebne
        // public List<Ocena> Oceny { get; set; }
    }

    public class Nauczyciel
    {
        public int NauczycielId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Przedmiot { get; set; }

        // Można dodać listę kursów prowadzonych przez nauczyciela, jeśli jest to potrzebne
        // public List<Kurs> Kursy { get; set; }
    }

    public class Kurs
    {
        public int KursId { get; set; }
        public string NazwaKursu { get; set; }

        // Klucz obcy dla nauczyciela
        public int NauczycielId { get; set; }
        // public Nauczyciel Nauczyciel { get; set; } 

        // Można dodać listę ocen dla kursu, jeśli jest to potrzebne
        // public List<Ocena> Oceny { get; set; }
    }

    public class Ocena
    {
        public int OcenaId { get; set; }
        public decimal Wartosc { get; set; }
        public DateTime Data { get; set; }

        // Klucze obce dla studenta i kursu
        public int StudentId { get; set; }
        // public Student Student { get; set; } 

        public int KursId { get; set; }
        // public Kurs Kurs { get; set; } 
    }

}