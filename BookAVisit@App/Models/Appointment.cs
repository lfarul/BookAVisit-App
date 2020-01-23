using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Appointment Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppointmentDate { get; set; }

        // navigation properties - wizyta może dotyczyć tylko jednego lekarza i pacjenta (lekarz przyjmuje jednego pacjenta)
        // podcza wizyty przyjmuje jeden lekarz
        public virtual Doctor Doctor { get; set; }
    }
}
