﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookAVisit_App.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please provide 11 digit identity number")]
        [RegularExpression(@"^[0-9]{11}$")]
        public string Pesel { get; set; }


        [Required(ErrorMessage = "Please provide first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
