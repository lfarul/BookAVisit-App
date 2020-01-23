using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.ViewModels
{
    public class AppointmentDetailsViewModel
    {
        public Appointment Appointment { get; set; }
        public int AppointmentID { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime? AppointmentDate { get; set; }
    }
}
