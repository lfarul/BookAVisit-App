using BookAVisit_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.ViewModels
{
    public class AppointmentViewModel
    {
        public AppointmentViewModel()

        {

            Users = new List<string>();
        }

        public int AppointmentID { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorID { get; set; }

        public string Specialty { get; set; }

        // patient
        public string UserName { get; set; }
        public string UserID { get; set; }

        [Required(ErrorMessage = "Please provide date of appointment")]
        [Display(Name = "Appointment Date")]
        public DateTime? AppointmentDate { get; set; }

        public List<string> Users { get; set; }

        public bool IsSelected { get; set; }
    }
}
