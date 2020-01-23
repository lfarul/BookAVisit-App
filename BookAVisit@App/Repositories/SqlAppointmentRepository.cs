using BookAVisit_App.DataContext;
using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.Repositories
{
    public class SqlAppointmentRepository : AppointmentRepository
    {
        private readonly ApplicationDbContext context;

        //ApplicationDbContext class injection by constructor method
        public SqlAppointmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Appointment Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
            return appointment;
        }

        public Appointment Delete(int id)
        {
            // before deleting Wizyta, we need to find them first
            Appointment appointment = context.Appointments.Find(id);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
            }
            return appointment;
        }

        public IEnumerable<Appointment> GetAllAppointment()
        {
            return context.Appointments;
        }

        public Appointment GetAppointment(int AppointmentID)
        {
            return context.Appointments.Find(AppointmentID);
        }

        public Appointment Update(Appointment appointmentUpdate)
        {
            var appointment = context.Appointments.Attach(appointmentUpdate);
            appointment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return appointmentUpdate;
        }
    }
}

