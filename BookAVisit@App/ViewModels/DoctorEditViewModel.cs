using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAVisit_App.ViewModels
{
    public class DoctorEditViewModel : DoctorCreateViewModel
    {
        public int DoctorID { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
