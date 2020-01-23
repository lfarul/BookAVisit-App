using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookAVisit_App.DataContext;
using BookAVisit_App.Models;
using BookAVisit_App.Repositories;
using BookAVisit_App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookAVisit_App.Controllers
{
    public class DoctorController : Controller
    {// dependency injection przez konstruktor
        private readonly DoctorRepository _doctorRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;

        public DoctorController(DoctorRepository doctorRepository, IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _doctorRepository = doctorRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            _context = context;
        }

        // List of doctors for a patient
        public ViewResult NewIndex()
        {
            var model = _doctorRepository.GetAllDoctor();
            return View(model);
        }

        // List of doctors for an administrator
        public ViewResult Index()
        {
            var model = _doctorRepository.GetAllDoctor();
            return View(model);
        }


        public ViewResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Doctor = _doctorRepository.GetDoctor(id),
                PageTitle = "Doctor Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Doctor doctor = _doctorRepository.GetDoctor(id);
            DoctorEditViewModel doctorEditViewModel = new DoctorEditViewModel
            {
                DoctorID = doctor.DoctorID,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                Description = doctor.Description,
                ExistingPhotoPath = doctor.PhotoPath
            };
            return View(doctorEditViewModel);
        }


        [HttpPost]
        public IActionResult Edit(DoctorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = _doctorRepository.GetDoctor(model.DoctorID);
                doctor.FirstName = model.FirstName;
                doctor.LastName = model.LastName;
                doctor.Specialty = model.Specialty;
                doctor.Email = model.Email;
                doctor.Description = model.Description;


                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    doctor.PhotoPath = ProcessUploadedFile(model);
                }

                _doctorRepository.Update(doctor);

                return RedirectToAction("NewIndex");
            }
            return View();
        }

        private string ProcessUploadedFile(DoctorCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(DoctorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Doctor newDoctor = new Doctor
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Specialty = model.Specialty,
                    Email = model.Email,
                    Description = model.Description,
                    PhotoPath = uniqueFileName
                };

                _doctorRepository.Add(newDoctor);

                return RedirectToAction("details", new { id = newDoctor.DoctorID });
            }
            return View();
        }

        public Doctor Delete(int id)
        {
            // before deleting a Pacjent, we need to find them first
            Doctor doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
            return doctor;
        }

    }
}
