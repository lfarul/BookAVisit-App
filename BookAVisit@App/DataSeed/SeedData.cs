using BookAVisit_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisitApp.DataSeed
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(

                new Doctor { DoctorID = 1, FirstName = "Konrad", LastName = "Kowalski", Specialty = "Internista", Email = "kkowalski@klinika" },
                new Doctor { DoctorID = 2, FirstName = "Agata", LastName = "Nowak", Specialty = "Laryngolog", Email = "anowak@klinika" },
                new Doctor { DoctorID = 3, FirstName = "Jan", LastName = "Kujawski", Specialty = "Dermatolog", Email = "jkujawski@klinika" },
                new Doctor { DoctorID = 4, FirstName = "Damian", LastName = "Borowicz", Specialty = "Stomatolog", Email = "dborowicz@klinika" },
                new Doctor { DoctorID = 5, FirstName = "Karolina", LastName = "Zawadzka", Specialty = "Okulista", Email = "kzawadzka@klinika" }
            );

        }

    }
}
