using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.Repositories
{
    public class AppointmentRepositoryImplementation : AppointmentRepository
    {
        private List<Appointment> _appointmentList;

        public AppointmentRepositoryImplementation()
        {
            _appointmentList = new List<Appointment>()
            {
               new Appointment() {AppointmentID = 1, DoctorID = 1, UserName = "lfarulewski@yahoo.com" , AppointmentDate = new DateTime (2020,1,20) },
               new Appointment() {AppointmentID = 2, DoctorID = 2, UserName = "jwayne@gmail.com" , AppointmentDate = new DateTime (2020,1,23) },
               new Appointment() {AppointmentID = 3, DoctorID = 3, UserName = "rlewandowski@gmail.com" , AppointmentDate = new DateTime (2020,1,30) },
               new Appointment() {AppointmentID = 4, DoctorID = 4, UserName = "rkubica@hotmail.com" , AppointmentDate = new DateTime (2020,1,15) },
               new Appointment() {AppointmentID = 5, DoctorID = 4, UserName = "jbravo@gmail.com" , AppointmentDate = new DateTime (2020,1,10) },
               new Appointment() {AppointmentID = 6, DoctorID = 3, UserName = "rkubica@gmail.com" , AppointmentDate = new DateTime (2020,4,12) }
            };
        }

        public Appointment Add(Appointment appointment)
        {
            appointment.AppointmentID = _appointmentList.Max(e => e.AppointmentID) + 1;

            _appointmentList.Add(appointment);
            return appointment;
        }

        public Appointment Delete(int id)
        {
            Appointment appointment = _appointmentList.FirstOrDefault(e => e.AppointmentID == id);
            if (appointment != null)
            {
                _appointmentList.Remove(appointment);
            }
            return appointment;
        }

        public IEnumerable<Appointment> GetAllAppointment()
        {
            return _appointmentList;
        }

        public Appointment GetAppointment(int id)
        {
            return _appointmentList.FirstOrDefault(e => e.AppointmentID == id);
        }

        public Appointment Update(Appointment appointmentUpdate)
        {
            Appointment appointment = _appointmentList.FirstOrDefault(e => e.AppointmentID == appointmentUpdate.AppointmentID);
            if (appointment != null)
            {
                appointment.DoctorID = appointmentUpdate.DoctorID;
                appointment.UserName = appointmentUpdate.UserName;
                appointment.AppointmentDate = appointmentUpdate.AppointmentDate;
            }
            return appointment;
        }
    }
}

