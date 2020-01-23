using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.Repositories
{
    public interface DoctorRepository
    {
        Doctor GetDoctor(int DoctorID);
        IEnumerable<Doctor> GetAllDoctor();
        Doctor Add(Doctor doctor);
        Doctor Update(Doctor doctorUpdate);
        Doctor Delete(int id);
    }
}
