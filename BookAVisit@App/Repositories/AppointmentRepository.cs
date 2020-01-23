using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.Repositories
{
    public interface AppointmentRepository
    {
        Appointment GetAppointment(int AppointmentID);
        IEnumerable<Appointment> GetAllAppointment();
        Appointment Add(Appointment appointment);
        Appointment Update(Appointment appointmentUpdate);
        Appointment Delete(int id);
    }
}
