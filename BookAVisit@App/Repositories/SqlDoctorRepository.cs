using BookAVisit_App.DataContext;
using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.Repositories
{
    public class SqlDoctorRepository : DoctorRepository
    {
        private readonly ApplicationDbContext context;

        //ApplicationDbContext class injection by constructor method
        public SqlDoctorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Doctor Add(Doctor doctor)
        {
            context.Doctors.Add(doctor);
            context.SaveChanges();
            return doctor;
        }

        public Doctor Delete(int id)
        {
            // before deleting a Doctor, we need to find them first
            Doctor doctor = context.Doctors.Find(id);
            if (doctor != null)
            {
                context.Doctors.Remove(doctor);
                context.SaveChanges();
            }
            return doctor;
        }

        public IEnumerable<Doctor> GetAllDoctor()
        {
            return context.Doctors;
        }

        public Doctor GetDoctor(int DoctorID)
        {
            return context.Doctors.Find(DoctorID);
        }

        public Doctor Update(Doctor doctorUpdate)
        {
            var doctor = context.Doctors.Attach(doctorUpdate);
            doctor.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return doctorUpdate;
        }
    }
}
